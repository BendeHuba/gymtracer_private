import { inject, Injectable } from '@angular/core';
import { OwnedTicketData } from '../userprofilemodels/userticket.model';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class Usertickets {
  http = inject(HttpClient);

  getOwnedTicketsOfUser(id: number) {
    return this.http.get<OwnedTicketData[]>(`${environment.apiUrl}/Ticket/user/${id}`);
  }

  payForOwnedTicket(id: number, paymentId: number) {
    return this.http.patch(`${environment.apiUrl}/Ticket/user/${id}/pay/${paymentId}`, {});
  }
}
