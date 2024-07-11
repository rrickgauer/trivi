import { IController } from "../../../domain/contracts/icontroller";
import { LoginForm } from "./login-form";


export class LoginPageController implements IController
{
    private _form: LoginForm;

    constructor()
    {
        this._form = new LoginForm();
    }

    public control()
    {
        this._form.control();
    }
}


