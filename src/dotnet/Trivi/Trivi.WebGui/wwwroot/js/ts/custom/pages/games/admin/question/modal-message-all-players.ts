import { NativeEvents } from "../../../../domain/constants/native-events";
import { OpenSendAllPlayersMessageModalEvent, SendAllPlayersMessageEvent } from "../../../../domain/events/events";
import { Selector } from "../../../../domain/helpers/element-selector/selector";
import { InputFeedbackText } from "../../../../domain/helpers/input-feedback";
import { SpinnerButton } from "../../../../domain/helpers/spinner-button";
import { BootstrapUtility } from "../../../../utility/bootstrap-utility";

const elements = {
    modalClass: '.modal-message-all-players',
    formClass: '.form-message-all-players',
    messageInputId: '#form-message-all-players-input',
}

export const ModalMessageAllPlayersElements = elements;

export class ModalMessageAllPlayers
{
    private static readonly _selector = Selector.fromString(elements.modalClass);
    private static readonly _modal = this._selector.element as HTMLDivElement;
    private static readonly _form = this._selector.querySelector<HTMLFormElement>(elements.formClass);
    private static readonly _messageInput = new InputFeedbackText(this._selector.querySelector(elements.messageInputId));
    private static readonly _btnSubmit = SpinnerButton.inParent(this._form, '.btn-submit');
    private static readonly _alertsContainer = this._selector.querySelector<HTMLDivElement>('.alerts-container');

    private static get _messageValue()
    {
        return this._messageInput.inputElement.value;
    }

    public static init()
    {
        this.addListeners();
    }

    private static addListeners()
    {

        OpenSendAllPlayersMessageModalEvent.addListener((m) =>
        {
            BootstrapUtility.showModal(this._modal);
        });


        this._form.addEventListener(NativeEvents.Submit, (e) =>
        {
            e.preventDefault();

            SendAllPlayersMessageEvent.invoke(this, {
                message: this._messageValue,
            });
        });

        this._messageInput.inputElement.addEventListener(NativeEvents.KeyUp, (e) =>
        {
            this._btnSubmit.button.disabled = this._messageInput.inputElement.value.length === 0;
        });

        this._messageInput.inputElement.addEventListener(NativeEvents.Change, (e) =>
        {
            this._btnSubmit.button.disabled = this._messageInput.inputElement.value.length === 0;
        });
    }

}