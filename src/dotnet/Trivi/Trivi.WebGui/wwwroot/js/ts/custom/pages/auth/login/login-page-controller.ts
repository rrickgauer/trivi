import { IController } from "../../../domain/contracts/icontroller";
import { UrlUtility } from "../../../utility/url-utility";
import { LoginForm } from "./login-form";


export class LoginPageController implements IController
{
    private _form: LoginForm;
    private _destinationParm: string;
    

    constructor()
    {
        this._destinationParm = UrlUtility.getQueryParmValue('destination') ?? '/app';
        this._form = new LoginForm(this._destinationParm);
        
    }

    public control()
    {
        this._form.control();
    }
}


