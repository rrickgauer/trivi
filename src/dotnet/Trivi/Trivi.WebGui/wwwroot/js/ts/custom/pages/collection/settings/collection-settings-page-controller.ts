import { NativeEvents } from "../../../domain/constants/native-events";
import { IController } from "../../../domain/contracts/icontroller";
import { EditCollectionFormSubmittedData, EditCollectionFormSubmittedEvent } from "../../../domain/events/events";
import { Selector } from "../../../domain/helpers/element-selector/selector";
import { MessageBoxConfirm } from "../../../domain/helpers/message-box/MessageBoxConfirm";
import { PostCollectionApiRequestBody } from "../../../domain/models/collection-models";
import { Guid } from "../../../domain/types/aliases";
import { CollectionsService } from "../../../services/collections-service";
import { AlertUtility } from "../../../utility/alert-utility";
import { ErrorUtility } from "../../../utility/error-utility";
import { EditCollectionForm } from "./edit-collection-form";



const elements = {
    pageContentClass: '.collection-page-content',
    btnDeleteCollectionClass: '.btn-delete-collection',
    dangerZoneAlertsContainer: '.danger-zone-alerts-container',
}

export class CollectionSettingsPageController implements IController
{
    private readonly _collectionId: string;
    private _editForm: EditCollectionForm;
    private _collectionsService: CollectionsService;
    private _selector: Selector;
    private _btnDeleteCollection: HTMLButtonElement;
    private _dangerZoneAlertsContainer: HTMLDivElement;

    constructor(collectionId: Guid)
    {
        this._collectionId = collectionId;

        this._selector = Selector.fromString(elements.pageContentClass);

        this._btnDeleteCollection = this._selector.querySelector<HTMLButtonElement>(elements.btnDeleteCollectionClass);
        this._dangerZoneAlertsContainer = this._selector.querySelector<HTMLDivElement>(elements.dangerZoneAlertsContainer);
        

        this._editForm = new EditCollectionForm();
        this._collectionsService = new CollectionsService();
    }


    public control()
    {
        this._editForm.control();   
        this.addListeners();
    }

    private addListeners = () =>
    {
        EditCollectionFormSubmittedEvent.addListener(async (message) =>
        {
            if (message.data)
            {
                await this.onEditCollectionFormSubmittedEvent(message.data);
            }  
        });

        this._btnDeleteCollection.addEventListener(NativeEvents.Click, async (e) =>
        {
            await this.handleDeleteCollection();
        });
    }

    private async onEditCollectionFormSubmittedEvent(message: EditCollectionFormSubmittedData)
    {
        this._editForm.showLoading();

        const isUpdated = await this.updateCollection({
            name: message.name,
        });

        if (isUpdated)
        {
            this._editForm.showSuccessfulAlert('Changes saved.');
        }

        this._editForm.showStandard();
    }


    private async updateCollection(data: PostCollectionApiRequestBody): Promise<boolean>
    {
        try
        {
            const response = await this._collectionsService.updateCollection(this._collectionId, data);

            if (!response.successful)
            {
                this._editForm.showErrorsAlert(response.response.errors);
                return false;
            }

            return true;
        }
        catch (error)
        {
            this.handleUpdateCollectionException(error);
            return false;
        }
    }


    private handleUpdateCollectionException(error: Error)
    {
        ErrorUtility.onException(error, {
            onOther: (e)                  => this._editForm.showDangerAlert('Unexpected error. Please try again later'),
            onApiValidationException: (e) => this._editForm.showDangerAlert('Validation error. Please try again later'),
            onApiNotFoundException: (e)   => this._editForm.showDangerAlert('This collection was not found. Please try again later.'),
            onApiForbiddenException: (e)  => this._editForm.showDangerAlert('You do not have permission to edit this collection.'),
        });

        console.error({ e: error });
    }



    private async handleDeleteCollection()
    {

        const confirmModal = new MessageBoxConfirm('Are you sure you want to permanetly delete this collection? This cannot be undone.');

        confirmModal.confirm({
            onSuccess: async () =>
            {
                try
                {
                    const response = await this._collectionsService.deleteCollection(this._collectionId);

                    if (!response.successful)
                    {
                        AlertUtility.showErrors({
                            container: this._dangerZoneAlertsContainer,
                            errors: response.response.errors,
                        });

                        return;
                    }
                    else
                    {
                        window.location.href = '/app/collections';
                    }
                }
                catch (error)
                {
                    ErrorUtility.onException(error, {
                        onApiNotFoundException: (e) => this.showDangerZoneBadAlert('Could not find this collection. Please try again later.'),
                        onOther: (e) => this.showDangerZoneBadAlert('Unexpected error. Please try again later.'),
                        onApiForbiddenException: (e) => this.showDangerZoneBadAlert('You do not have permission to delete this collection.'),
                    });

                    console.error(error);
                }
            },
        });

    }

    private showDangerZoneBadAlert(message: string)
    {
        AlertUtility.showDanger({
            container: this._dangerZoneAlertsContainer,
            message: message,
        });
    }
    
}