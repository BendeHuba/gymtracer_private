export interface GymStatDayItem {
  date: string;
  guestNumber: number;
}
export interface GymStatWeekItem {
  startOfWeek: string;
  guestNumber: number;
}
export interface GymStatResponse {
  dayBackReturn: GymStatDayItem[];
  weekBackReturn: GymStatWeekItem[];
}