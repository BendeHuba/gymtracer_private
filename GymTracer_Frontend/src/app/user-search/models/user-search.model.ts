import { UserRole } from "../../models/user.role.model";

export type UserSearchResult = {
  id: number;
  name: string;
  email: string;
  role: UserRole;
}