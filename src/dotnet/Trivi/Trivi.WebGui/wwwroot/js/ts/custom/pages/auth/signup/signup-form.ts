import { NativeEvents } from "../../../domain/constants/native-events";
import { IController } from "../../../domain/contracts/icontroller";
import { ApiErrorCode } from "../../../domain/enums/api-error-codes";
import { InputFeebackState } from "../../../domain/enums/input-feedback-state";
import { Selector } from "../../../domain/helpers/element-selector/selector";
import { InputFeedbackText } from "../../../domain/helpers/input-feedback";
import { SpinnerButton } from "../../../domain/helpers/spinner-button";
import { ErrorMessage } from "../../../domain/models/api-response";
import { SignupApiRequest } from "../../../domain/models/auth-models";
import { AuthService } from "../../../services/auth-service";
import { AlertUtility } from "../../../utility/alert-utility";
import { ErrorUtility } from "../../../utility/error-utility";
import { MessageBoxUtility } from "../../../utility/message-box-utility";


const selectors = {
    formClass: '.form-signup',
    emailInputId: '#form-signup-input-email',
    passwordInputId: '#form-signup-input-password',
    passwordConfirmInputId: '#form-signup-input-password-confirm',
    submitBtnClass: '.btn-submit',
    alertsContainerClass: '.alerts-container',
}


export class SignupForm implements IController
{


    private _form: HTMLFormElement;
    private _email: InputFeedbackText;
    private _password: InputFeedbackText;
    private _passwordConfirm: InputFeedbackText;
    private _submitBtn: SpinnerButton;
    private _fieldSet: HTMLFieldSetElement;
    private _authService: AuthService;
    private _alertsContainer: HTMLDivElement;
    private _destination: string;
    private _selector: Selector;

    private get _emailInputValue(): string
    {
        return this._email.inputElement.value;
    }

    private get _passwordInputValue(): string
    {
        return this._password.inputElement.value;
    }

    private get _passwordConfirmInputValue(): string
    {
        return this._passwordConfirm.inputElement.value;
    }

    constructor(destination: string)
    {
        this._destination = destination;

        this._selector = Selector.fromString(selectors.formClass);

        this._form = this._selector.closest<HTMLFormElement>(selectors.formClass);
        this._email = new InputFeedbackText(this._selector.querySelector(selectors.emailInputId), true);
        this._password = new InputFeedbackText(this._selector.querySelector(selectors.passwordInputId), true);
        this._passwordConfirm = new InputFeedbackText(this._selector.querySelector(selectors.passwordConfirmInputId), true);
        this._submitBtn = new SpinnerButton(this._selector.querySelector<HTMLButtonElement>(selectors.submitBtnClass));
        this._fieldSet = this._selector.querySelector<HTMLFieldSetElement>('fieldset');
        this._alertsContainer = this._selector.querySelector<HTMLDivElement>(selectors.alertsContainerClass);

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
            await this.handleFormSubmission();
        });
    }


    private async handleFormSubmission()
    {
        try
        {
            this.resetInputFeedbackStates();
            this._submitBtn.spin();
            this._fieldSet.disabled = true;

            const data = this.getFormData();

            const response = await this._authService.signup(data);

            this._submitBtn.reset();
            this._fieldSet.disabled = false;

            if (!response.successful)
            {
                this.handleBadSignupRequest(response.response.errors);
                return;
            }

            window.location.href = this._destination;

        }
        catch (error)
        {
            this._submitBtn.reset();
            this._fieldSet.disabled = false;

            ErrorUtility.onException(error, {
                onOther: (e) =>
                {
                    MessageBoxUtility.showError({
                        message: 'Unexpected error.',
                    });

                    console.error({ e });
                },
            });
        }
    }


    private resetInputFeedbackStates()
    {
        this._email.state = InputFeebackState.None;
        this._password.state = InputFeebackState.None;
        this._passwordConfirm.state = InputFeebackState.None;
    }


    private handleBadSignupRequest(errors: ErrorMessage[])
    {
        const otherErrors: ErrorMessage[] = [];

        for (const e of errors)
        {
            switch (e.id)
            {
                case ApiErrorCode.AuthEmailTaken:
                    this._email.showInvalid(e.message);
                    return;

                case ApiErrorCode.AuthPasswordCriteriaNotMet:
                case ApiErrorCode.AuthPasswordsNotMatch:
                    this._password.state = InputFeebackState.Invalid;
                    this._passwordConfirm.showInvalid(e.message);
                    return;

                default:
                    otherErrors.push(e);
                    break;
            }
        }

        AlertUtility.showErrors({
            container: this._alertsContainer,
            errors: otherErrors,
        });
    }



    private getFormData(): SignupApiRequest
    {
        return {
            email: this._emailInputValue,
            password: this._passwordInputValue,
            passwordConfirm: this._passwordConfirmInputValue,
        }
    }
}


