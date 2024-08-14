import { PlayerQuestionResponseApiResponse } from "../domain/models/player-models";
import { PlayerStatusListItemElements } from "../pages/games/admin/question/player-status-list-item";
import { HtmlTemplate } from "./html-template";



export class PlayerQuestionResponseTemplate extends HtmlTemplate<PlayerQuestionResponseApiResponse>
{
    private readonly _checkboxIcon = `<i class='bx bxs-checkbox-checked' style = 'color:#22da11'></i>`;    
    private readonly _emptyIcon = `<i class='bx bxs-checkbox' style='color:#d5d8d5'></i>`;
    private readonly _containerClass = PlayerStatusListItemElements.containerClass.substring(1);
    private readonly _nicknameClass = PlayerStatusListItemElements.playerNicknameClass.substring(1);

    public toHtml(data: PlayerQuestionResponseApiResponse)
    {
        const checkbox = data.hasResponse ? this._checkboxIcon : this._emptyIcon;

        let html = `
            <li class="list-group-item ${this._containerClass}" data-player-id="${data.id}">
                <div class="row">
                    <div class="col-lg-4">
                        <a title="Settings" class="text-decoration-none hover-pointer fw-bolder ${this._nicknameClass}">${data.nickname}</a>
                    </div>

                    <div class="col-lg-4">
                        <div class="text-body-secondary user-select-all">${data.id}</div>
                    </div>

                    <div class="col-lg-4 text-end">
                        ${checkbox}
                    </div>
                </div>
                
            </li>
        `;

        return html;
    }
}