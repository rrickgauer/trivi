import { OpenModalPlayerQuestionSettingsEvent } from "../../../../domain/events/events";
import { Selector } from "../../../../domain/helpers/element-selector/selector";
import { Guid } from "../../../../domain/types/aliases";
import { BootstrapUtility } from "../../../../utility/bootstrap-utility";



const elements = {
    containerClass: '.player-status-list-item',
    playerNicknameClass: '.player-list-item-nickname',
    playerIdAttr: 'data-player-id',
}


export const PlayerStatusListItemElements = elements;


export class PlayerStatusListItem
{
    private _selector: Selector;
    private _container: HTMLLIElement;
    private _nickname: HTMLAnchorElement;

    public get playerId(): Guid
    {
        return this._container.getAttribute(elements.playerIdAttr)!;
    }

    constructor(e: Element)
    {
        this._selector = Selector.fromClosest(elements.containerClass, e);
        this._container = this._selector.element as HTMLLIElement;
        this._nickname = this._selector.querySelector<HTMLAnchorElement>(elements.playerNicknameClass);
    }


    public openSettingsModal()
    {
        OpenModalPlayerQuestionSettingsEvent.invoke(this, {
            playerId: this.playerId,
        });
    }

}