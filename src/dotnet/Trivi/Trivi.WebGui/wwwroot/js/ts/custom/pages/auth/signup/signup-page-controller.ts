import { IController } from "../../../domain/contracts/icontroller";
import { UrlUtility } from "../../../utility/url-utility";
import { SignupForm } from "./signup-form";



export class SignupPageController implements IController
{
    private _form: SignupForm;
    private _destination: string;

    constructor()
    {
        this._destination = UrlUtility.getQueryParmValue('destination') ?? '/app';
        this._form = new SignupForm(this._destination);
    }

    public control()
    {
        this._form.control();
    }
}