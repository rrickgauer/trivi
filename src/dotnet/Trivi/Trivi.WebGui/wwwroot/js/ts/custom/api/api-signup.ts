import { HttpMethods } from "../domain/constants/api-constants";
import { ApplicationTypes } from "../domain/constants/application-types";
import { SignupApiRequest } from "../domain/models/auth-models";
import { MappingUtility } from "../utility/mapping-utility";
import { ApiEndpoints } from "./api-base";



export class ApiSignup 
{
    private _url: string;

    constructor() 
    {
        this._url = ApiEndpoints.Signup;
    }

    public async post(data: SignupApiRequest) 
    {
        const url = this._url;

        return await fetch(url, {
            method: HttpMethods.POST,
            headers: ApplicationTypes.GetJsonHeaders(),
            body: MappingUtility.toJson(data),
        });
    }
}
