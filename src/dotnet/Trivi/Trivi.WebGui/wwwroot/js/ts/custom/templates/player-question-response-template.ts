import { PlayerQuestionResponseApiResponse } from "../domain/models/player-models";
import { HtmlTemplate } from "./html-template";



export class PlayerQuestionResponseTemplate extends HtmlTemplate<PlayerQuestionResponseApiResponse>
{
    public toHtml(data: PlayerQuestionResponseApiResponse)
    {
        let html = `


        <tr data-player-id="${data.id}">
            <td>${data.nickname}</td>
            <td class="user-select-all">${data.id}</td>
            <td>${data.hasResponse}</td>
            <td>
                <div class="dropdown">
                    <button class="btn btn-sm btn-reset" type="button" data-bs-toggle="dropdown">&ctdot;</button>
                    <ul class="dropdown-menu">
                        <li><button class="dropdown-item" type="button">Action</button></li>
                        <li><button class="dropdown-item" type="button">Another action</button></li>
                        <li><button class="dropdown-item" type="button">Something else here</button></li>
                    </ul>
                </div>
            </td>
        </tr>



        `;

        return html;
    }
}