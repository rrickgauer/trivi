import { IController } from "../../../domain/contracts/icontroller";
import { PlayerJoinedGameData, PlayerJoinedGameEvent } from "../../../domain/events/events";
import { UrlUtility } from "../../../utility/url-utility";
import { JoinGameForm } from "./join-game-form";



export class JoinGamePageController implements IController
{
    private readonly _joinGameForm: JoinGameForm;

    constructor()
    {
        this._joinGameForm = new JoinGameForm();
    }

    public control()
    {
        this._joinGameForm.control();

        this.addListeners();
    }

    private addListeners = () =>
    {
        PlayerJoinedGameEvent.addListener((message) =>
        {
            this.onPlayerJoinedGameEvent(message.data!);
        });
    }

    private onPlayerJoinedGameEvent(message: PlayerJoinedGameData)
    {
        const path = `/games/${message.player.gameId}`;

        const url = UrlUtility.createUrlFromSuffix(path);

        url.searchParams.set('player', message.player.id);

        window.location.href = url.toString();
    }
}