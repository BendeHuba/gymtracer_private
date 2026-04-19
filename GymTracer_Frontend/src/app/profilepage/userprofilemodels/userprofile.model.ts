import { UserRole } from "../../models/user.role.model";

export interface UserProfileModel {
    id:           number;
    name:         string;
    email:        string;
    birthDate:    Date | null;
    creationDate: Date;
    role:         UserRole;
    wentInToday:  boolean;
}
