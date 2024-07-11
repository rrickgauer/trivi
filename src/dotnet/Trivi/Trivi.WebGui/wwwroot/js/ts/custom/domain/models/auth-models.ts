import { DateTimeString, Guid } from "../types/aliases";



export type LoginApiRequest = {
    email: string;
    password: string;
}

export type UserApiResponse = {
    userId: Guid | null;
    userEmail: string | null;
    userCreatedOn: DateTimeString | null;
}