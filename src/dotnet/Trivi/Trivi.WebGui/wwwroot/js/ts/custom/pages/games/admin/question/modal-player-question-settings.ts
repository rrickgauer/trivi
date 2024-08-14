import { NativeEvents } from "../../../../domain/constants/native-events";
import { OpenModalPlayerQuestionSettingsData, OpenModalPlayerQuestionSettingsEvent, SendPlayerToastEvent } from "../../../../domain/events/events";
import { Selector } from "../../../../domain/helpers/element-selector/selector";
import { InputFeedbackText } from "../../../../domain/helpers/input-feedback";
import { SpinnerButton } from "../../../../domain/helpers/spinner-button";
import { BootstrapUtility } from "../../../../utility/bootstrap-utility";




const elements = {
    modalClass: '.modal-player-question-settings',
    playerIdDisplayClass: '.player-id',
    alertsContainerClass: '.alerts-container',
    messageFormClass: '.form-player-question-message',
    messageInputId: '#form-player-question-message-input',
}

export const ModalPlayerQuestionSettingsElements = elements;

export class ModalPlayerQuestionSettings
{
    private static readonly _selector = Selector.fromString(elements.modalClass);
    private static readonly _container = this._selector.element as HTMLDivElement;
    private static readonly _playerIdDisplay = this._selector.querySelector<HTMLDivElement>(elements.playerIdDisplayClass);
    private static readonly _form = this._selector.querySelector<HTMLFormElement>(elements.messageFormClass);
    private static readonly _fieldset = this._selector.querySelector<HTMLFieldSetElement>(elements.messageFormClass);
    private static readonly _messageInput = new InputFeedbackText(this._selector.querySelector(elements.messageInputId));
    private static readonly _btnSubmit = SpinnerButton.inParent(this._container, '.btn-submit');

    private static _playerId = "";

    public static init()
    {
        this.addListeners();
    }

    private static get _messageInputValue(): string
    {
        return this._messageInput.inputElement.value;
    }

    private static addListeners()
    {
        OpenModalPlayerQuestionSettingsEvent.addListener((message) =>
        {
            this.showPlayer(message.data!);
        });


        this._form.addEventListener(NativeEvents.Submit, (e) =>
        {
            e.preventDefault();
            this.onMessageFormSubmit();
        });

        // disable submit button if message input is empty
        this._messageInput.inputElement.addEventListener(NativeEvents.KeyUp, (e) =>
        {
            this._btnSubmit.button.disabled = this._messageInput.inputElement.value.length === 0;
        });

        // disable submit button if message input is empty
        this._messageInput.inputElement.addEventListener(NativeEvents.Change, (e) =>
        {
            this._btnSubmit.button.disabled = this._messageInput.inputElement.value.length === 0;
        });

        
    }

    private static showPlayer(data: OpenModalPlayerQuestionSettingsData)
    {
        this._playerId = data.playerId;

        this._playerIdDisplay.innerText = this._playerId;

        BootstrapUtility.showModal(this._container);
    }


    private static async onMessageFormSubmit()
    {
        SendPlayerToastEvent.invoke(this, {
            message: this._messageInputValue,
            playerId: this._playerId,
        });

        
        this._messageInput.inputElement.value = "";
        this._btnSubmit.button.disabled = true;
    }

}