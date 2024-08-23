import bootstrap from "bootstrap";
import { NativeEvents } from "../../../../domain/constants/native-events";
import { IController, IControllerAsync } from "../../../../domain/contracts/icontroller";
import { AdminUpdatePlayerQuestionResponsesEvent, OpenSendAllPlayersMessageModalEvent, SendAllPlayersMessageEvent, SendPlayerToastEvent } from "../../../../domain/events/events";
import { Selector } from "../../../../domain/helpers/element-selector/selector";
import { SpinnerButton } from "../../../../domain/helpers/spinner-button";
import { QuestionId } from "../../../../domain/types/aliases";
import { GameQuestionHub } from "../../../../hubs/game-question/game-question-hub";
import { AdminUpdatePlayerQuestionResponsesParms } from "../../../../hubs/game-question/models";
import { GameQuestionService } from "../../../../services/game-question-service";
import { PlayerQuestionResponseTemplate } from "../../../../templates/player-question-response-template";
import { AlertUtility } from "../../../../utility/alert-utility";
import { ErrorUtility } from "../../../../utility/error-utility";
import { PageUtility } from "../../../../utility/page-utility";
import { UrlUtility } from "../../../../utility/url-utility";
import { ModalMessageAllPlayers } from "./modal-message-all-players";
import { ModalPlayerQuestionSettings } from "./modal-player-question-settings";
import { PlayerStatusListItem, PlayerStatusListItemElements } from "./player-status-list-item";
import { ClipboardUtility } from "../../../../utility/clipboard-utility";
import { ToastUtility } from "../../../../utility/toast-utility";


export type AdminQuestionPageUrlParms = {
    gameId: string;
    questionId: QuestionId;
}


const elements = {
    tableContainerClass:  ".player-status-table-container",
    tableClass: ".player-status-list",
    btnCloseQuestionClass: ".btn-close-question",
    alertsContainerClass: ".alerts-container",
    btnMessageAllPlayersClass: '.btn-message-all-players',
    btnSetQuestionTimerClass: ".btn-set-question-timer",
    timerSecondsAttr: "data-timer-seconds",
    btnOpenSetQuestionTimerClass: ".btn-open-set-question-timer",
    btnCopyJoinGameLinkClass: ".btn-copy-join-game-link",
}

export const AdminQuestionPageControllerElements = elements;

export class AdminQuestionPageController implements IControllerAsync
{
    private _urlParms: AdminQuestionPageUrlParms;
    private _gameHub: GameQuestionHub;
    private _selector: Selector;
    private _playersList: HTMLUListElement;
    private _htmlEngine: PlayerQuestionResponseTemplate;
    private _btnCloseQuestion: SpinnerButton;
    private _alertsContainer: HTMLDivElement;
    private _btnMessageAllPlayers: SpinnerButton;
    private _btnOpenSetQuestionTimer: SpinnerButton;
    private _btnCopyJoinGameLink: SpinnerButton;

    constructor(urlParms: AdminQuestionPageUrlParms)
    {
        this._urlParms = urlParms;

        this._gameHub = new GameQuestionHub(this._urlParms.gameId);

        this._selector = Selector.fromString(elements.tableContainerClass);
        this._playersList = this._selector.querySelector<HTMLUListElement>(elements.tableClass);
        this._htmlEngine = new PlayerQuestionResponseTemplate();
        this._btnCloseQuestion = SpinnerButton.inParent(this._selector.element, elements.btnCloseQuestionClass);
        this._alertsContainer = this._selector.querySelector<HTMLDivElement>(elements.alertsContainerClass);
        this._btnMessageAllPlayers = SpinnerButton.inParent(this._selector.element, elements.btnMessageAllPlayersClass);
        this._btnOpenSetQuestionTimer = SpinnerButton.inParent(this._selector.element, elements.btnOpenSetQuestionTimerClass);
        this._btnCopyJoinGameLink = SpinnerButton.inParent(this._selector.element, elements.btnCopyJoinGameLinkClass);
    }

    public async control()
    {
        this.addListeners();

        await this._gameHub.control();
        await this._gameHub.adminJoinQuestionPage(this._urlParms.questionId);

        ModalPlayerQuestionSettings.init();
        ModalMessageAllPlayers.init();
    }

    private addListeners()
    {
        AdminUpdatePlayerQuestionResponsesEvent.addListener((message) =>
        {
            this.onAdminUpdatePlayerQuestionResponsesEvent(message.data!);
        });

        this._btnCloseQuestion.button.addEventListener(NativeEvents.Click, async (e) =>
        {
            await this.onBtnCloseQuestionClicked();
        });

        // listen for player list item click
        this._playersList.addEventListener(NativeEvents.Click, (e) =>
        {
            const target = e.target as Element;

            if (!target) return;

            if (target.classList.contains(PlayerStatusListItemElements.playerNicknameClass.substring(1)))
            {
                e.preventDefault();
                this.onPlayerListItemClick(target);
            }
        });

        SendPlayerToastEvent.addListener(async (message) =>
        {
            await this._gameHub.adminSendPlayerMessage({
                message: message.data!.message,
                playerId: message.data!.playerId,
            });
        });


        this._btnMessageAllPlayers.button.addEventListener(NativeEvents.Click, (e) =>
        {
            this.onBtnMessageAllPlayersClick();
        });

        SendAllPlayersMessageEvent.addListener(async (message) =>
        {
            await this._gameHub.adminSendAllPlayersMessage({
                message: message.data!.message,
            });
        });

        this._selector.element.querySelectorAll<HTMLButtonElement>(elements.btnSetQuestionTimerClass)?.forEach((button) =>
        {
            button.addEventListener(NativeEvents.Click, async (e) =>
            {
                const seconds = parseInt(button.getAttribute(elements.timerSecondsAttr)!);
                await this.setQuestionTimer(seconds);
            });
        });


        this._btnCopyJoinGameLink.button.addEventListener(NativeEvents.Click, (e) =>
        {
            this.onBtnCopyJoinGameLinkClick();
        });
    }



    private onAdminUpdatePlayerQuestionResponsesEvent(message: AdminUpdatePlayerQuestionResponsesParms)
    {
        this._playersList.innerHTML = this._htmlEngine.toHtmls(message.responses);
    }

    private async onBtnCloseQuestionClicked()
    {
        try
        {
            await this.closeQuestion();
        }
        catch (error)
        {
            this._btnCloseQuestion.reset();
            this.onCloseQuestionException(error);
        }
    }

    private async closeQuestion()
    {
        this._btnCloseQuestion.spin();

        const gameQuestionService = new GameQuestionService(this._urlParms.gameId);

        const response = await gameQuestionService.closeQuestion(this._urlParms.questionId);

        this._btnCloseQuestion.reset();

        if (!response.successful)
        {
            AlertUtility.showErrors({
                container: this._alertsContainer,
                errors: response.response.errors,
            });

            return;
        }

        const newPath = `/games/admin/${this._urlParms.gameId}`;
        window.location.href = UrlUtility.replacePath(newPath).toString();
    }

    private onCloseQuestionException(error: Error)
    {
        ErrorUtility.onException(error, {
            onApiNotFoundException: (e) => this.showErrorAlert('Question not found.'),
            onApiForbiddenException: (e) => this.showErrorAlert('Closing this question is forbidden.'),
            onOther: (e) => this.showErrorAlert('Unexpected error. Please try again later.'),
        });

        console.error(error);
    }


    private showErrorAlert(message: string)
    {
        AlertUtility.showDanger({
            container: this._alertsContainer,
            message: message,
        });
    }


    private onPlayerListItemClick(e: Element)
    {
        const playerListItem = new PlayerStatusListItem(e);
        playerListItem.openSettingsModal();
    }


    private onBtnMessageAllPlayersClick()
    {
        OpenSendAllPlayersMessageModalEvent.invoke(this);
    }


    private async setQuestionTimer(seconds: number)
    {
        // hide the dropdown menu
        const dropdownMenu = bootstrap.Dropdown.getOrCreateInstance(this._btnOpenSetQuestionTimer.button.closest('.dropup')!);
        dropdownMenu.hide();

        // disable button
        this._btnOpenSetQuestionTimer.spin();

        // notify players that the question has a timer
        await this._gameHub.adminSendAllPlayersMessage({
            message: `Question closing in ${seconds} seconds!`,
        });

        // close the question after specified seconds
        const milliseconds = seconds * 1000;
            
        setTimeout(async () =>
        {
            await this.onBtnCloseQuestionClicked();
            this._btnOpenSetQuestionTimer.reset();
        }, milliseconds);
    }


    private onBtnCopyJoinGameLinkClick()
    {
        const url = this.getJoinGameUrl();

        ClipboardUtility.copyToClipboard(url.toString());

        ToastUtility.showSuccess({
            message: 'Copied to clipboard.',
        });
    }

    private getJoinGameUrl(): URL
    {
        const url = new URL(window.location.href);
        url.pathname = '/games';

        for (const key of url.searchParams.keys())
        {
            url.searchParams.delete(key);
        }

        url.searchParams.set('gameId', this._urlParms.gameId);

        return url;
    }
}