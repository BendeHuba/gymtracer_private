export interface TicketModel {
  id: number;
  type: number;
  description: string;
  isStudent: boolean;
  price: number;
  maxUsage: number;
  trainingId: number;
  trainerName: string;
}