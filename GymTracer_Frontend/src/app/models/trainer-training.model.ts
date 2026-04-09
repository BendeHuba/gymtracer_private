export type TrainerTrainingModel = {
  id: number;
  name: string;
  description: string;
  image: string;
  startTime: string;
  endTime: string;
  maxParticipant: number;
  trainer: {
    id: number;
    name: string;
  };
}
export type CreateTrainingDto = {
  name: string;
  description: string;
  image: string;
  startTime: string;
  endTime: string;
  maxParticipant: number;
  tickets: CreateTicketDto[];
}
export type CreateTicketDto = {
  id?: number;
  description: string;
  isStudent: boolean;
  price: number;
  type: number;
}
export type UpdateTrainingDto = CreateTrainingDto;
