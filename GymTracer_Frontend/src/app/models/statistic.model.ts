export type GymStatDayItem = {
  date: string;
  guestNumber: number;
}
export type GymStatWeekItem = {
  startOfWeek: string;
  guestNumber: number;
}
export type GymStatResponse = {
  dayBackReturn: GymStatDayItem[];
  weekBackReturn: GymStatWeekItem[];
}
export type TicketStatItem = {
  soldAmount: number;
  ticket: {
    type: number;
    description: string;
    isStudent: boolean;
    price: number;
    tax_key: number;
    maxUsage: number;
  }
}
export type CardLogItem = {
  useDate: string;
  cardId: number;
  userId: number;
  name: string;
  email: string;
}
