

export type AuthApiRequest = {
    email: string;
    password: string;
}

export type LoginApiRequest = AuthApiRequest;

export type SignupApiRequest = AuthApiRequest & {
    passwordConfirm: string;
}