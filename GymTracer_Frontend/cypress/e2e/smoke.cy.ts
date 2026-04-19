describe('GymTracer app', () => {
  it('loads the home page shell', () => {
    cy.visit('/');
    cy.get('app-root').should('exist');
  });
});
