import { Ticket } from "./training.ticket.model";
import { TrainingUser } from "./training.user.model";

export type TrainingDetailResponse = {
  id: number;
  name: string;
  isApplied: boolean;
  description: string;
  image: string;
  startTime: string;
  endTime: string;
  maxParticipant: number;
  trainer: {
    id: number;
    name: string;
  };
  tickets: Ticket[];
  users?: TrainingUser[];
}