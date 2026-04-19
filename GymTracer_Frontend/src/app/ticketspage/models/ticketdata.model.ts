import { TicketType } from "./tickettype.model";

export interface TicketData {
    id:          number;
    type:        TicketType;
    description: string;
    isStudent:   boolean;
    price:       number;
    maxUsage:    number;
    trainingId:  number | null;
    trainingName: string | null;
}

