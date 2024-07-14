
import { NativeEvents } from "../../domain/constants/native-events";
import { OpenNewCollectionModal } from "../../domain/events/events";
import { DocumentSelector, Selector } from "../../domain/helpers/element-selector/selector";
import { InputFeedback, InputFeedbackText } from "../../domain/helpers/input-feedback";
import { SpinnerButton } from "../../domain/helpers/spinner-button";
import { PostCollectionApiRequestBody } from "../../domain/models/collection-models";
import { CollectionsService } from "../../services/collections-service";
import { AlertUtility } from "../../utility/alert-utility";
import { BootstrapUtility } from "../../utility/bootstrap-utility";
import { ErrorUtility } from "../../utility/error-utility";



const elements = {
    modalClass: '.new-collection-modal',
    inputNameId: '#new-collection-form-input-name',
    alertsContainerClass: '.alerts-container',
    btnSubmitClass: '.btn-submit',
    btnCancelClass: '.btn-cancel',
    formClass: '.new-collection-form',
}

export class NewCollectionModal 
{

    private static readonly _selector = Selector.fromString(elements.modalClass);
    private static readonly _container = this._selector.element as HTMLDivElement;
    private static readonly _form = this._selector.querySelector<HTMLFormElement>(elements.formClass);
    private static readonly _name = new InputFeedbackText(this._selector.querySelector<HTMLInputElement>(elements.inputNameId));
    private static readonly _alertsContainer = this._selector.querySelector<HTMLDivElement>(elements.alertsContainerClass);
    private static readonly _btnSubmit = SpinnerButton.inParent(this._container, elements.btnSubmitClass);
    private static readonly _btnCancel = SpinnerButton.inParent(this._container, elements.btnCancelClass);

    private static readonly _collectionsService = new CollectionsService();

    private static get _bootstrapModal()
    {
        return BootstrapUtility.getModal(this._container);
    }

    private static get _nameInputValue()
    {
        return this._name.inputElement.value;
    }

    public static control()
    {
        this.addListeners();
    }

    private static addListeners = () =>
    {
        OpenNewCollectionModal.addListener(() =>
        {
            this._bootstrapModal.show();
        });

        this._btnCancel.button.addEventListener(NativeEvents.Click, (e) =>
        {
            this._bootstrapModal.hide();
        });


        this._form.addEventListener(NativeEvents.Submit, async (e) =>
        {
            e.preventDefault();
            await this.handleFormSubmission();
        })
    }


    private static async handleFormSubmission()
    {
        try
        {
            this._btnSubmit.spin();

            const data: PostCollectionApiRequestBody = {
                name: this._nameInputValue,
            }

            const response = await this._collectionsService.createCollection(data);

            if (!response.successful)
            {
                AlertUtility.showErrors({
                    container: this._alertsContainer,
                    errors: response.response.errors,
                });

                return;
            }

            if (response.response.data?.uriGui)
            {
                window.location.href = response.response.data.uriGui;
            }

        }
        catch (error)
        {
            console.error({ error });

            ErrorUtility.onException(error, {
                onOther: (e) =>
                {
                    this.showErrorAlert('Unexpected error. Please try again later.');

                },
            });
        }
        finally
        {
            this._btnSubmit.reset();
        }
    }




    private static showErrorAlert(message: string)
    {
        AlertUtility.showDanger({
            container: this._alertsContainer,
            message: message,
        });
    }

    private static showSuccessfulAlert(message: string)
    {
        AlertUtility.showSuccess({
            container: this._alertsContainer,
            message: message,
        });
    }
}





