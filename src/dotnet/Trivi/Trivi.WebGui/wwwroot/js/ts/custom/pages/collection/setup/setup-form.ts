import { NativeEvents } from "../../../domain/constants/native-events";
import { IController } from "../../../domain/contracts/icontroller";
import { QuestionTimeLimitOption } from "../../../domain/enums/question-time-limit-option";
import { GameCreatedEvent } from "../../../domain/events/events";
import { Selector } from "../../../domain/helpers/element-selector/selector";
import { RadioGroup } from "../../../domain/helpers/radio-group/radio-group";
import { SpinnerButton } from "../../../domain/helpers/spinner-button";
import { GameApiResponse, GameApiPostRequest } from "../../../domain/models/game-models";
import { Guid } from "../../../domain/types/aliases";
import { GameService } from "../../../services/game-service";
import { AlertUtility } from "../../../utility/alert-utility";
import { ErrorUtility } from "../../../utility/error-utility";
import { FormUtility } from "../../../utility/form-utility";

const elements = {
    containerClass: ".form-setup-game-container",
    alertsContainerClass: ".alerts-container",
    formClass: ".form-setup-game",

    randomizeQuestionsInputId: "#form-setup-game-input-randomize",

    timeLimitInputName: "form-setup-game-input-time-limit",

    timeLimitInputNoneId: "#form-setup-game-input-time-limit-none",
    timeLimitInput15Id: "#form-setup-game-input-time-limit-15",
    timeLimitInput30Id: "#form-setup-game-input-time-limit-30",
    timeLimitInput60Id: "#form-setup-game-input-time-limit-60",
}

export class SetupForm implements IController
{
    private _selector: Selector;
    private _container: HTMLDivElement;
    private _form: HTMLFormElement;
    private _alertsContainer: HTMLDivElement;
    private _randomizeQuestionsInput: HTMLInputElement;
    private _timeLimitRadioGroup: RadioGroup<QuestionTimeLimitOption>;
    private _fieldset: HTMLFieldSetElement;
    private _btnSubmit: SpinnerButton;
    private _gameService: GameService;
    private _collectionId: string;


    private get _randomizeQuestions(): boolean
    {
        return FormUtility.isChecked(this._randomizeQuestionsInput);
    }

    private get _timeLimit(): number | null
    {
        switch (this._timeLimitRadioGroup.selectedValue)
        {
            case QuestionTimeLimitOption.None:
                return null;
            case QuestionTimeLimitOption.Fifteen:
                return 15;
            case QuestionTimeLimitOption.Thirty:
                return 30;
            case QuestionTimeLimitOption.Sixty:
                return 60;
            default:
                return null;
        }
    }

    constructor(collectionId: Guid)
    {
        this._collectionId = collectionId;

        this._selector = Selector.fromString(elements.containerClass);
        this._container = this._selector.element as HTMLDivElement;

        this._form = this._selector.querySelector<HTMLFormElement>(elements.formClass);
        this._alertsContainer = this._selector.querySelector<HTMLDivElement>(elements.alertsContainerClass);
        this._randomizeQuestionsInput = this._selector.querySelector<HTMLInputElement>(elements.randomizeQuestionsInputId);
        this._timeLimitRadioGroup = new RadioGroup<QuestionTimeLimitOption>(this._selector.querySelector('.radio-group'));
        this._fieldset = this._selector.querySelector<HTMLFieldSetElement>('fieldset');
        this._btnSubmit = SpinnerButton.inParent(this._container, '.btn-submit');

        this._gameService = new GameService();
    }


    public control()
    {
        this.addListeners();
    }

    private addListeners = () =>
    {
        this._form.addEventListener(NativeEvents.Submit, async (e) =>
        {
            e.preventDefault();
            await this.onFormSubmit();
        });
    }

    private async onFormSubmit()
    {
        const game = await this.createGame();

        if (game)
        {
            GameCreatedEvent.invoke(this, {
                game: game,
            });
        }
    }


    private async createGame(): Promise<GameApiResponse | null>
    {
        try
        {
            this._btnSubmit.spin();
            this._fieldset.disabled = true;

            const gameData = this.getGameData();
            const response = await this._gameService.createGame(gameData);

            this._btnSubmit.reset();
            this._fieldset.disabled = false;

            if (!response.successful)
            {
                AlertUtility.showErrors({
                    container: this._alertsContainer,
                    errors: response.response.errors,
                });

                return null;
            }


            return response.response.data;

        }
        catch (error)
        {
            this._btnSubmit.reset();
            this._fieldset.disabled = false;

            this.handleCreateGameException(error);

            return null;
        }
    }


    private getGameData(): GameApiPostRequest
    {
        const gameData: GameApiPostRequest = {
            collectionId: this._collectionId,
            questionTimeLimit: this._timeLimit,
            randomizeQuestions: this._randomizeQuestions,
        }

        return gameData;
    }

    private handleCreateGameException(error: Error)
    {
        ErrorUtility.onException(error, {
            onApiForbiddenException: (e) => this.showErrorAlert('You do not have permission to create a game with this collection.'),
            onApiNotFoundException: (e) => this.showErrorAlert('Collection not found.'),
            onOther: (e) => this.showErrorAlert('Unexpected error. Please try again later.'),
        });
    }

    private showErrorAlert(message: string)
    {
        AlertUtility.showDanger({
            container: this._alertsContainer,
            message: message,
        });
    }



  
}