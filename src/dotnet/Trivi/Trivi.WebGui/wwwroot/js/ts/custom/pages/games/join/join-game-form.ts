import { NativeEvents } from "../../../domain/constants/native-events";
import { IController } from "../../../domain/contracts/icontroller";
import { ApiErrorCode } from "../../../domain/enums/api-error-codes";
import { InputFeebackState } from "../../../domain/enums/input-feedback-state";
import { PlayerJoinedGameEvent } from "../../../domain/events/events";
import { Selector } from "../../../domain/helpers/element-selector/selector";
import { InputFeedbackText } from "../../../domain/helpers/input-feedback";
import { SpinnerButton } from "../../../domain/helpers/spinner-button";
import { ErrorMessage } from "../../../domain/models/api-response";
import { PlayerApiPostRequest, PlayerApiResponse } from "../../../domain/models/player-models";
import { PlayerService } from "../../../services/player-service";
import { AlertUtility } from "../../../utility/alert-utility";
import { ErrorUtility } from "../../../utility/error-utility";

const elements = {
    containerClass: '.form-join-game-container',
    formClass: '.form-join-game',
    gameIdInputId: '#form-join-game-input-game-id',
    nicknameInputId: '#form-join-game-input-nickname',
    alertsContainerClass: '.alerts-container',
}

export class JoinGameForm implements IController
{
    private _selector: Selector;
    private _container: HTMLDivElement;
    private _form: HTMLFormElement;
    private _inputGameId: InputFeedbackText;
    private _inputNickname: InputFeedbackText;
    private _alertsContainer: HTMLDivElement;
    private _fieldset: HTMLFieldSetElement;
    private _btnSubmit: SpinnerButton;
    private _playerService: PlayerService;


    private get _gameIdValue(): string
    {
        return this._inputGameId.inputElement.value;
    }

    private get _nicknameValue(): string
    {
        return this._inputNickname.inputElement.value;
    }

    constructor()
    {
        this._selector = Selector.fromString(elements.containerClass);
        this._container = this._selector.element as HTMLDivElement;

        this._form = this._selector.querySelector<HTMLFormElement>(elements.formClass);

        this._inputGameId = new InputFeedbackText(this._selector.querySelector<HTMLInputElement>(elements.gameIdInputId));
        this._inputNickname = new InputFeedbackText(this._selector.querySelector<HTMLInputElement>(elements.nicknameInputId));

        this._alertsContainer = this._selector.querySelector<HTMLDivElement>(elements.alertsContainerClass);
        this._fieldset = this._selector.querySelector<HTMLFieldSetElement>('fieldset');
        this._btnSubmit = SpinnerButton.inParent(this._container, '.btn-submit');

        this._playerService = new PlayerService();
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
            const player = await this.joinGame();

            if (player)
            {
                PlayerJoinedGameEvent.invoke(this, {
                    player: player,
                });
            }
        });
    }



    private async joinGame(): Promise<PlayerApiResponse | null>
    {
        try
        {
            this._inputGameId.state = InputFeebackState.None;
            this._inputNickname.state = InputFeebackState.None;

            this._btnSubmit.spin();
            this._fieldset.disabled = true;

            const formData = this.getFormData();
            const response = await this._playerService.joinGame(formData);

            this._btnSubmit.reset();
            this._fieldset.disabled = false;

            if (!response.successful)
            {
                this.handleJoinGameErrorMessages(response.response.errors);
                return null;
            }

            return response.response.data;
        }
        catch (error)
        {
            this._btnSubmit.reset();
            this._fieldset.disabled = false;

            this.onJoinGameException(error);

            return null;
        }
    }


    private handleJoinGameErrorMessages(errors: ErrorMessage[])
    {
        const otherErrors: ErrorMessage[] = [];

        let gameIdIsValid = true;
        let nicknameIsValid = true;

        for (const errorMessage of errors)
        {
            switch (errorMessage.id)
            {
                case ApiErrorCode.JoinGameNotFound:
                case ApiErrorCode.JoinGameAlreadyFinished:
                    this._inputGameId.showInvalid(errorMessage.message);
                    gameIdIsValid = false;
                    break;

                case ApiErrorCode.JoinGameNicknameAlreadyTaken:
                case ApiErrorCode.JoinGameInvalidNicknameLength:
                    this._inputNickname.showInvalid(errorMessage.message);
                    nicknameIsValid = false;
                    break;

                default:
                    otherErrors.push(errorMessage);
                    break;
            }
        }

        

        if (otherErrors.length > 0)
        {
            AlertUtility.showErrors({
                container: this._alertsContainer,
                errors: otherErrors,
            });

            return;
        }

        if (gameIdIsValid)
        {
            this._inputGameId.state = InputFeebackState.Valid;
        }

        if (nicknameIsValid)
        {
            this._inputNickname.state = InputFeebackState.Valid;
        }
    }





    private getFormData()
    {
        const result: PlayerApiPostRequest = {
            gameId: this._gameIdValue,
            nickname: this._nicknameValue,
        }

        return result;
    }

    private onJoinGameException(error: Error)
    {
        ErrorUtility.onException(error, {
            onApiForbiddenException: (e) =>
            {
                this._inputGameId.showInvalid('This game is currently not accepting new members.');
            },

            onApiNotFoundException: (e) =>
            {
                this._inputGameId.showInvalid('This game could not be found.');
            },

            onOther: (e) =>
            {
                this.showErrorAlert('Unexpected error. Please try refreshing the page.');
            },
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



export const JoinGameFormElements = elements;