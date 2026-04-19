import { TicketType } from "./tickettype.model";

export interface OwnedTicketData {
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

