type TicketData = {
  id: number;
  type: number;
  description: string;
  isStudent: boolean;
  price: number;
  maxUsage: number;
  trainingId: number | null;
  trainingName: string | null;
};

type OwnedTicketData = {
  type: number;
  description: string;
  isStudent: boolean;
  expirationDate: string;
  price: number;
  paymentId: number;
  isPayed: boolean;
  usagesLeft: number | null;
  trainingId: number | null;
  trainingName: string | null;
};

type FixtureData = {
  tickets: TicketData[];
  ownedTickets: OwnedTicketData[];
};

const actingUser = {
  id: 42,
  name: 'E2E User',
  email: 'e2e@example.com',
  role: 0,
};

const defaultTickets: TicketData[] = [
  {
    id: 1,
    type: 1,
    description: 'Napijegy',
    isStudent: false,
    price: 3000,
    maxUsage: 1,
    trainingId: null,
    trainingName: null,
  },
  {
    id: 2,
    type: 2,
    description: 'Diak havi berlet',
    isStudent: true,
    price: 9500,
    maxUsage: 30,
    trainingId: null,
    trainingName: null,
  },
  {
    id: 3,
    type: 0,
    description: 'TRX ora',
    isStudent: false,
    price: 3500,
    maxUsage: 1,
    trainingId: 123,
    trainingName: 'TRX',
  },
  {
    id: 4,
    type: 0,
    description: 'Student spinning',
    isStudent: true,
    price: 2500,
    maxUsage: 1,
    trainingId: 124,
    trainingName: 'Spinning',
  },
];

const defaultOwnedTickets: OwnedTicketData[] = [
  {
    type: 2,
    description: 'Aktiv berlet',
    isStudent: false,
    expirationDate: '2030-12-01T00:00:00.000Z',
    price: 15000,
    paymentId: 501,
    isPayed: true,
    usagesLeft: null,
    trainingId: null,
    trainingName: null,
  },
  {
    type: 1,
    description: 'Fizetesre varo jegy',
    isStudent: false,
    expirationDate: '2030-12-15T00:00:00.000Z',
    price: 3000,
    paymentId: 502,
    isPayed: false,
    usagesLeft: 1,
    trainingId: null,
    trainingName: null,
  },
];

function setAuthenticatedSession(win: Window) {
  win.localStorage.setItem('auth_token', 'e2e-token');
  win.localStorage.setItem('token_valid_to', '2099-01-01T00:00:00.000Z');
  win.localStorage.setItem('current_user', JSON.stringify(actingUser));
  win.localStorage.setItem('staff_mode', 'false');
}

function mockTicketApis(tickets = defaultTickets, owned = defaultOwnedTickets) {
  cy.intercept('GET', '**/api/Ticket', {
    statusCode: 200,
    body: tickets,
  }).as('getTickets');

  cy.intercept('GET', '**/api/Ticket/user/*', {
    statusCode: 200,
    body: owned,
  }).as('getOwnedTickets');
}

function openTicketsPage() {
  cy.visit('/tickets', {
    onBeforeLoad: setAuthenticatedSession,
  });

  cy.wait('@getTickets');
  cy.wait('@getOwnedTickets');
}

function fillPaymentForm() {
  cy.get('#cardName').type('Kiss Bela');
  cy.get('#cardNumber').type('4111 1111 1111 1111');
  cy.get('#cardExpiry').type('12/30');
  cy.get('#cardCvc').type('123');
}

function openPurchaseModal(ticketDescription: string) {
  cy.contains('app-tickets-card', ticketDescription).within(() => {
    cy.get('a').first().click();
  });
}

describe('Tickets page e2e interactions', () => {
  it('shows owned empty-state message when user has no owned tickets', () => {
    mockTicketApis(defaultTickets, []);
    openTicketsPage();

    cy.contains(/Nincsenek.*jegyek/i).should('be.visible');
  });

  it('lists only non-student standard tickets by default', () => {
    mockTicketApis();
    openTicketsPage();

    cy.contains('Napijegy').should('be.visible');
    cy.contains('Diak havi berlet').should('not.exist');
  });

  it('toggles standard student filter and refreshes ticket list', () => {
    mockTicketApis();
    openTicketsPage();

    cy.get('#studentFilter1').check({ force: true });
    cy.wait('@getTickets');

    cy.contains('Diak havi berlet').should('be.visible');
    cy.contains('Napijegy').should('not.exist');
  });

  it('toggles training student filter and shows student training ticket', () => {
    mockTicketApis();
    openTicketsPage();

    cy.get('#studentFilter2').check({ force: true });
    cy.wait('@getTickets');

    cy.contains('Student spinning').should('be.visible');
    cy.contains('TRX ora').should('not.exist');
  });

  it('opens and closes ticket modal from standard ticket purchase', () => {
    mockTicketApis();
    openTicketsPage();

    openPurchaseModal('Napijegy');
    cy.get('app-ticket-modal').should('be.visible');

    cy.get('app-ticket-modal button').first().click();
    cy.get('app-ticket-modal').should('not.exist');
  });

  it('navigates to training details from training ticket card', () => {
    mockTicketApis();
    openTicketsPage();

    cy.contains('app-tickets-card', 'TRX ora').within(() => {
      cy.get('a').first().click();
    });
    cy.location('pathname').should('eq', '/trainings/123');
  });

  it('buys ticket without pay-now and sends POST with false payment flag', () => {
    mockTicketApis();
    cy.intercept('POST', '**/api/Ticket/1/user/42/false', {
      statusCode: 200,
      body: {},
    }).as('buyUnpaidTicket');

    openTicketsPage();

    openPurchaseModal('Napijegy');
    cy.get('app-ticket-modal button').last().click();

    cy.wait('@buyUnpaidTicket');
    cy.get('app-ticket-modal').should('not.exist');
  });

  it('shows validation error in payment modal for invalid form submit', () => {
    mockTicketApis();
    openTicketsPage();

    openPurchaseModal('Napijegy');
    cy.get('#payNow').check({ force: true });
    cy.get('app-ticket-modal button').last().click();
    cy.get('app-payment-modal button').last().click();

    cy.get('app-payment-modal .border-4').should('be.visible');
  });

  it('spies on console.log when ticket refresh fails', () => {
    mockTicketApis();
    openTicketsPage();

    cy.window().then((win) => {
      cy.spy(win.console, 'log').as('consoleLogSpy');
    });

    cy.intercept('GET', '**/api/Ticket', {
      statusCode: 500,
      body: {},
    }).as('getTicketsError');

    cy.get('#studentFilter1').check({ force: true });
    cy.wait('@getTicketsError');
    cy.get('@consoleLogSpy').should('have.been.called');
  });

  it('uses fixture data and stubs paid flows for new and owned ticket payment', () => {
    cy.fixture('tickets-page').then((fixtureData: FixtureData) => {
      mockTicketApis(fixtureData.tickets, fixtureData.ownedTickets);

      cy.intercept('POST', '**/api/Ticket/11/user/42/true', {
        statusCode: 200,
        body: {},
      }).as('buyPaidTicket');

      cy.intercept('PATCH', '**/api/Ticket/user/42/pay/992', {
        statusCode: 200,
        body: {},
      }).as('payOwnedTicket');

      openTicketsPage();

      cy.window().then((win) => {
        cy.stub(win.console, 'log').as('consoleLogStub');
      });

      openPurchaseModal('Napijegy');
      cy.get('#payNow').check({ force: true });
      cy.get('app-ticket-modal button').last().click();
      fillPaymentForm();
      cy.get('app-payment-modal button').last().click();
      cy.wait('@buyPaidTicket');

      cy.contains('app-owned-ticket-card', 'Fizetesre varo napijegy').within(() => {
        cy.get('a').first().click();
      });
      fillPaymentForm();
      cy.get('app-payment-modal button').last().click();
      cy.wait('@payOwnedTicket');

      cy.get('@consoleLogStub').should('not.have.been.called');
    });
  });
});
