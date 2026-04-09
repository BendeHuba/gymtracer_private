import { UserRole } from "./user.role.model"

export type AuthUserModel = {
  email: string,
  role: UserRole
}