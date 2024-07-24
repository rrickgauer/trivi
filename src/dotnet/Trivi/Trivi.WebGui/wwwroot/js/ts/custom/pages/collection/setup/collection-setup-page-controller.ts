import { IController } from "../../../domain/contracts/icontroller";
import { GameCreatedData, GameCreatedEvent } from "../../../domain/events/events";
import { Guid } from "../../../domain/types/aliases";
import { SetupForm } from "./setup-form";



export class CollectionSetupPageController implements IController
{
    private _collectionId: string;
    private _form: SetupForm;

    constructor(collectionId: Guid)
    {
        this._collectionId = collectionId;

        this._form = new SetupForm(this._collectionId);
    }

    public control()
    {
        this._form.control();
        this.addListeners();
    }

    private addListeners = () =>
    {
        GameCreatedEvent.addListener((message) =>
        {
            this.onGameCreatedEvent(message.data!);
        });
    }

    private onGameCreatedEvent(message: GameCreatedData)
    {
        const adminUrl = `/games/${message.game.id}/admin`;
        window.location.href = adminUrl;
    }
}