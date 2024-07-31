import { PlayerApiResponse } from "../domain/models/player-models";
import { HtmlTemplate } from "./html-template";



export class PlayersLobbyListTemplate extends HtmlTemplate<PlayerApiResponse>
{
    public toHtml(data: PlayerApiResponse)
    {
        let html = `

        <li>${data.nickname}</li>

        `;


        return html;
    }
}