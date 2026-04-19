export enum TicketType {
  "not_found" = -1,
  "training" = 0,
  "daily" = 1,
  "monthly" = 2,
  "x_usage" = 3
}

export type OwnedTicketData = {
  type:           TicketType;
  description:    string;
  isStudent:      boolean;
  expirationDate: Date;
  price:          number;
  paymentId:       number;
  isPayed:         boolean;
  usagesLeft:     number | null;
  trainingId:     number | null;
  trainingName:    string | null;
}