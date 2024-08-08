import { NativeEvents } from "../../../../domain/constants/native-events";
import { IController, IControllerAsync } from "../../../../domain/contracts/icontroller";
import { AdminUpdatePlayerQuestionResponsesEvent } from "../../../../domain/events/events";
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


export type AdminQuestionPageUrlParms = {
    gameId: string;
    questionId: QuestionId;
}


const elements = {
    tableContainerClass:  ".player-status-table-container",
    tableClass: ".player-status-table",
    btnCloseQuestionClass: ".btn-close-question",
    alertsContainerClass: ".alerts-container",
}

export const AdminQuestionPageControllerElements = elements;

export class AdminQuestionPageController implements IControllerAsync
{
    private _urlParms: AdminQuestionPageUrlParms;
    private _gameHub: GameQuestionHub;
    private _selector: Selector;
    private _table: HTMLTableElement;
    private _htmlEngine: PlayerQuestionResponseTemplate;
    private _btnCloseQuestion: SpinnerButton;
    private _alertsContainer: HTMLDivElement;

    constructor(urlParms: AdminQuestionPageUrlParms)
    {
        this._urlParms = urlParms;

        this._gameHub = new GameQuestionHub(this._urlParms.gameId);

        this._selector = Selector.fromString(elements.tableContainerClass);
        this._table = this._selector.querySelector<HTMLTableElement>(elements.tableClass);
        this._htmlEngine = new PlayerQuestionResponseTemplate();
        this._btnCloseQuestion = SpinnerButton.inParent(this._selector.element, elements.btnCloseQuestionClass);
        this._alertsContainer = this._selector.querySelector<HTMLDivElement>(elements.alertsContainerClass);
        
    }

    public async control()
    {
        this.addListeners();

        await this._gameHub.startConnection();
        await this._gameHub.adminJoinQuestionPage(this._urlParms.questionId);
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
    }

    private onAdminUpdatePlayerQuestionResponsesEvent(message: AdminUpdatePlayerQuestionResponsesParms)
    {
        if (this._table.tBodies[0])
        {
            this._table.tBodies[0].innerHTML = this._htmlEngine.toHtmls(message.responses);
        }
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
}