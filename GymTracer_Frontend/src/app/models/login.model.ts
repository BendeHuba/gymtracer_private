import { UserModel } from "./user.model";

export type UserLoginDto = {
    message: string;
    user:    UserModel;
    token:   string;
    validTo: Date;
}

export type LoginCredentials = {
    email: string;
    password: string;
}

