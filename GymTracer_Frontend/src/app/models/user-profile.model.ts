export type UserProfileModel = {
  user: {
    name: string;
    email: string;
    birthDate: string | null;
    creationDate: string;
    cards: number[];
  }
}
