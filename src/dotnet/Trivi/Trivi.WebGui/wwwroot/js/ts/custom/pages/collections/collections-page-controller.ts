import { NativeEvents } from "../../domain/constants/native-events";
import { IController } from "../../domain/contracts/icontroller";
import { OpenNewCollectionModal } from "../../domain/events/events";
import { Selector } from "../../domain/helpers/element-selector/selector";
import { NewCollectionModal } from "./new-collection-modal";



const selectors = {
    openNewCollectionModalClass: '.btn-open-new-collection-modal',
}


export class CollectionsPageController implements IController
{
    private _selector: Selector;
    private _btnOpenNewCollectionModal: HTMLButtonElement;

    constructor()
    {
        this._selector = new Selector();

        this._btnOpenNewCollectionModal = this._selector.querySelector<HTMLButtonElement>(selectors.openNewCollectionModalClass);
    }


    public control()
    {
        this.addListeners();
        NewCollectionModal.control();
    }

    private addListeners = () =>
    {
        this._btnOpenNewCollectionModal.addEventListener(NativeEvents.Click, (e) =>
        {
            OpenNewCollectionModal.invoke(null);
        });
    }
}