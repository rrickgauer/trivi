import { ApiLogin } from "../api/api-login";
import { ServiceResponse } from "../domain/models/api-response";
import { LoginApiRequest, UserApiResponse } from "../domain/models/auth-models";
import { ServiceUtility } from "../utility/service-utility";



export class AuthService
{

    public async login(data: LoginApiRequest)
    {
        const api = new ApiLogin();

        const response = await api.post(data);

        return ServiceUtility.toServiceResponseNoContent(response);
    }


}