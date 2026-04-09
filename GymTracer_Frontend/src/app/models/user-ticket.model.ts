import { TicketType } from '../trainingdetails/models/training.tickettype.model';
export type UserTicketModel = {
  type: TicketType;
  description: string;
  isStudent: boolean;
  expirationDate: string;
  usagesLeft: number;
  trainingId: number | null;
  trainerName: string | null;
}
export type UnpaidTicketModel = {
  paymentId: number;
  type: TicketType;
  description: string;
  isStudent: boolean;
  expirationDate: string;
  price: number;
  usagesLeft: number;
}
