import { UserRole } from "./user.role.model";

export type UserModel = {
    id:    number;
    name:  string;
    email: string;
    role:  UserRole;
}