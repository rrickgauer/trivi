import { NativeEvents } from "../../../domain/constants/native-events";
import { IController } from "../../../domain/contracts/icontroller";
import { EditCollectionFormSubmittedEvent } from "../../../domain/events/events";
import { Selector } from "../../../domain/helpers/element-selector/selector"
import { InputFeedbackText } from "../../../domain/helpers/input-feedback";
import { SpinnerButton } from "../../../domain/helpers/spinner-button";
import { ErrorMessage } from "../../../domain/models/api-response";
import { AlertUtility } from "../../../utility/alert-utility";




const elements = {
    formClass: '.edit-collection-form',
    alertsContainerClass: '.alerts-container',
    submitBtnClass: '.btn-submit',
    nameInputId: '#edit-collection-form-input-name'
}


export class EditCollectionForm implements IController
{
    private readonly _selector: Selector;
    private readonly _form: HTMLDivElement;
    private readonly _alertsContainer: HTMLDivElement;
    private readonly _fieldset: HTMLFieldSetElement;
    private readonly _name: InputFeedbackText;
    private readonly _btnSubmit: SpinnerButton;

    public get nameInputValue(): string
    {
        return this._name.inputElement.value;
    }

    public set nameInputValue(value: string)
    {
        this._name.inputElement.value = value;
    }


    private set _isSubmitBtnDisabled(value: boolean)
    {
        this._btnSubmit.button.disabled = value;
    }

    constructor()
    {
        this._selector = Selector.fromString(elements.formClass);

        this._form = this._selector.element as HTMLDivElement;
        this._alertsContainer = this._selector.querySelector<HTMLDivElement>(elements.alertsContainerClass);
        this._fieldset = this._selector.querySelector<HTMLFieldSetElement>('fieldset');
        this._btnSubmit = new SpinnerButton(this._selector.querySelector<HTMLButtonElement>(elements.submitBtnClass));
        this._name = new InputFeedbackText(this._selector.querySelector<HTMLInputElement>(elements.nameInputId));
    }

    public control()
    {
        this.addListeners();
    }


    public showLoading()
    {
        this._btnSubmit.spin();
        this._fieldset.disabled = true;
    }

    public showStandard()
    {
        this._btnSubmit.reset();
        this._fieldset.disabled = false;
    }

    public showSuccessfulAlert(message: string)
    {
        AlertUtility.showSuccess({
            container: this._alertsContainer,
            message: message,
        });
    }

    public showDangerAlert(message: string)
    {
        AlertUtility.showDanger({
            container: this._alertsContainer,
            message: message,
        });
    }

    public showErrorsAlert(errors: ErrorMessage[])
    {
        AlertUtility.showErrors({
            container: this._alertsContainer,
            errors: errors,
        });
    }

    



    private addListeners = () =>
    {
        this._form.addEventListener(NativeEvents.Submit, (e) =>
        {
            e.preventDefault();
            this.handleFormSubmission();
        });

        this._name.inputElement.addEventListener(NativeEvents.KeyUp, (e) =>
        {
            this.handleNameInputChange();
        });
    }

    // disable the submit button if name input is empty
    private handleNameInputChange()
    {
        if (this.nameInputValue.length === 0)
        {
            this._isSubmitBtnDisabled = true;
        }
        else
        {
            this._isSubmitBtnDisabled = false;
        }
    }

    private handleFormSubmission()
    {
        EditCollectionFormSubmittedEvent.invoke(this, {
            name: this.nameInputValue,
        });
    }


}