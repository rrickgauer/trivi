import { NativeEvents } from "../../../../domain/constants/native-events";
import { IController, IControllerAsync } from "../../../../domain/contracts/icontroller";
import { AdminLobbyUpdatedEvent, NavigateToEvent } from "../../../../domain/events/events";
import { Selector } from "../../../../domain/helpers/element-selector/selector";
import { SpinnerButton } from "../../../../domain/helpers/spinner-button";
import { AdminGameHub } from "../../../../hubs/game/game-hub";
import { AdminLobbyUpdatedData } from "../../../../hubs/game/models";
import { GameService } from "../../../../services/game-service";
import { PlayersLobbyListTemplate } from "../../../../templates/players-lobby-list-template";
import { ErrorUtility } from "../../../../utility/error-utility";
import { MessageBoxUtility } from "../../../../utility/message-box-utility";

const elements = {
    containerClass: '.players-lobby-list-container',
    playersListClass: '.players-lobby-list',
    btnStartGameClass: '.btn-start-game',
}

export class AdminLobbyPageController implements IControllerAsync
{
    private readonly _gameId: string;
    private _adminGameHub: AdminGameHub;
    private _selector: Selector;
    private _playersList: HTMLUListElement;
    private _htmlEngine: PlayersLobbyListTemplate;
    private _btnStartGame: SpinnerButton;
    private _gameService: GameService;

    constructor(gameId: string)
    {
        this._gameId = gameId;

        this._adminGameHub = new AdminGameHub({
            gameId: this._gameId,
        });


        this._selector = Selector.fromString(elements.containerClass);
        this._playersList = this._selector.querySelector<HTMLUListElement>(elements.playersListClass);
        this._btnStartGame = SpinnerButton.inParent(this._selector.element, elements.btnStartGameClass);

        this._htmlEngine = new PlayersLobbyListTemplate();
        this._gameService = new GameService();
    }

    public async control()
    {
        this.addListeners();
        await this._adminGameHub.control();
    }

    private addListeners = () =>
    {
        AdminLobbyUpdatedEvent.addListener((message) =>
        {
            this.onAdminLobbyUpdatedEvent(message.data!);
        });

        this._btnStartGame.button.addEventListener(NativeEvents.Click, async (e) =>
        {
            await this.startGame();
        });

        NavigateToEvent.addListener((message) =>
        {
            window.location.href = message.data?.data?.destination!;
        });
    }

    private onAdminLobbyUpdatedEvent(message: AdminLobbyUpdatedData)
    {
        this._playersList.innerHTML = this._htmlEngine.toHtmls(message.players);
    }

    private async startGame()
    {
        try
        {
            this._btnStartGame.spin();

            const response = await this._gameService.startGame(this._gameId);

            console.log({ game: response.response.data });

            if (!response.successful)
            {
                this._btnStartGame.reset();
                MessageBoxUtility.showErrorList(response.response.errors);
            }

            this._btnStartGame.reset();

        }
        catch (error)
        {
            this._btnStartGame.reset();

            ErrorUtility.onException(error, {
                onOther: (e) =>
                {
                    MessageBoxUtility.showError({
                        message: 'Error starting the game.',
                    });
                },
            });
        }
    }
}