import { NativeEvents } from "../../../domain/constants/native-events";
import { IController } from "../../../domain/contracts/icontroller";
import { ApiErrorCode } from "../../../domain/enums/api-error-codes";
import { InputFeebackState } from "../../../domain/enums/input-feedback-state";
import { InputFeedbackText } from "../../../domain/helpers/input-feedback";
import { SpinnerButton } from "../../../domain/helpers/spinner-button";
import { ErrorMessage } from "../../../domain/models/api-response";
import { LoginApiRequest } from "../../../domain/models/auth-models";
import { AuthService } from "../../../services/auth-service";
import { ErrorUtility } from "../../../utility/error-utility";
import { MessageBoxUtility } from "../../../utility/message-box-utility";


const selectors = {

    emailInputId: '#form-login-input-email',
    passwordInputId: '#form-login-input-password',
    formClass: '.form-login',
    btnSubmitClass: '.btn-submit',
    alertsContainerClass: '.alerts-container',
}

export class LoginForm implements IController
{
    private readonly _form: HTMLFormElement;
    private readonly _email: InputFeedbackText;
    private readonly _password: InputFeedbackText;
    private readonly _btnSubmit: SpinnerButton;
    private readonly _authService: AuthService;
    private readonly _alertsContainer: HTMLDivElement;
    private readonly _destination: string;

    constructor(destination: string)
    {
        this._destination = destination;

        this._form = document.querySelector<HTMLFormElement>(selectors.formClass);
        this._email = new InputFeedbackText(this._form.querySelector(selectors.emailInputId), true);
        this._password = new InputFeedbackText(this._form.querySelector(selectors.passwordInputId), true);
        this._btnSubmit = new SpinnerButton(this._form.querySelector<HTMLButtonElement>(selectors.btnSubmitClass));
        this._alertsContainer = this._form.querySelector<HTMLDivElement>(selectors.alertsContainerClass);

        this._authService = new AuthService();
    }


    public control()
    {
        this.addListeners();
    }

    private addListeners = () =>
    {
        this._form.addEventListener(NativeEvents.Submit, async (e) =>
        {
            e.preventDefault();
            await this.handleFormSubmit();
        });
    }


    private async handleFormSubmit()
    {
        try
        {
            this._btnSubmit.spin();
            const loginData = this.getFormData();

            const response = await this._authService.login(loginData);

            this._btnSubmit.reset();

            if (!response.successful)
            {
                this.handleBadLoginRequest(response.response.errors);
                return;
            }

            window.location.href = this._destination;
        }
        catch (error)
        {
            this.handleLoginError(error);
        }
    }


    private getFormData(): LoginApiRequest
    {
        return {
            email: this._email.inputElement.value,
            password: this._password.inputElement.value,
        }
    }


    private handleBadLoginRequest(errors: ErrorMessage[])
    {
        const message = errors.find(m => m.id === ApiErrorCode.AuthInvalidEmailOrPassword);

        this._email.state = InputFeebackState.Invalid;

        if (message)
        {
            this._password.showInvalid(message.message);
        }
        else
        {
            this._password.showInvalid('Invalid login credentials');
        }
    }

    private handleLoginError(error: Error)
    {
        ErrorUtility.onException(error, {
            onOther: (e) =>
            {
                MessageBoxUtility.showError({
                    message: 'Please try again later.',
                    title: 'Unexpected Error',
                });
            },
        });
    }

}