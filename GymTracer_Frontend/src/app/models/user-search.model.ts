import { UserRole } from './user.role.model';
export type UserSearchResult = {
  id: number;
  name: string;
  email: string;
  role: UserRole;
}
