import { ApiLogin } from "../api/api-login";
import { ApiSignup } from "../api/api-signup";
import { LoginApiRequest, SignupApiRequest } from "../domain/models/auth-models";
import { ServiceUtility } from "../utility/service-utility";


export class AuthService
{

    public async login(data: LoginApiRequest)
    {
        const api = new ApiLogin();

        const response = await api.post(data);

        return ServiceUtility.toServiceResponseNoContent(response);
    }

    public async signup(data: SignupApiRequest)
    {
        const api = new ApiSignup();

        const response = await api.post(data);

        return ServiceUtility.toServiceResponseNoContent(response);
    }


}