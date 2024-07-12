import { Nullable } from "../../utility/nullable";
import { NativeEvents } from "../constants/native-events";
import { InputFeebackState } from "../enums/input-feedback-state";
import { Selector } from "./element-selector/selector";



// SEE: https://getbootstrap.com/docs/5.3/forms/validation/#server-side

export class InputFeedback
{
    private static readonly IsValidText = "is-valid";
    private static readonly IsInvalidText = "is-invalid";

    private _container: HTMLDivElement;
    private _validFeedbackElement: HTMLDivElement;
    private _invalidFeedbackElement: HTMLDivElement;
    public inputElement: HTMLElement;
    private _selector: Selector;

    /**
     * Helper class for interfacing with an input's valid feedback text/display
     * @param e
     */
    constructor(e: Element, autoClear: boolean=true)
    {
        this._selector = Selector.fromClosest<HTMLDivElement>('.input-feedback', e);
        this._container = this._selector.element as HTMLDivElement;
        this._validFeedbackElement = this._selector.querySelector<HTMLDivElement>('.valid-feedback');
        this._invalidFeedbackElement = this._selector.querySelector<HTMLDivElement>('.invalid-feedback');
        this.inputElement = this._selector.querySelector<HTMLElement>('.input-feedback-input');

        if (autoClear)
        {
            this.inputElement?.addEventListener(NativeEvents.KeyPress, (e) =>
            {
                this.clearInputFeedbackClasses();
            });
        }
    }


    /**
     * Set the valid text and show it as valid
     * @param text the text to display
     */
    public showValid = (text: string) =>
    {
        this.validFeedbackText = text;
        this.state = InputFeebackState.Valid;
    }

    /**
     * Show the invalid text
     * @param text the text to display
     */
    public showInvalid = (text: string) =>
    {
        this.invalidFeedbackText = text;
        this.state = InputFeebackState.Invalid;
    }

    /**
     * Get the current InputFeedbackState
     */
    public get state(): InputFeebackState
    {
        if (this.inputElement.classList.contains(InputFeedback.IsInvalidText))
        {
            return InputFeebackState.Invalid;
        }
        else if (this.inputElement.classList.contains(InputFeedback.IsValidText))
        {
            return InputFeebackState.Valid;
        }
        else
        {
            return InputFeebackState.None;
        }
    }

    /**
     * Set the state
     */
    public set state(value: InputFeebackState)
    {
        this.clearInputFeedbackClasses();

        switch (value)
        {
            case InputFeebackState.Valid:
                this.inputElement.classList.add(InputFeedback.IsValidText);
                break;

            case InputFeebackState.Invalid:
                this.inputElement.classList.add(InputFeedback.IsInvalidText);
                break;

            default:
                break;
        }
    }

    /**
     * Get the current valid feedback text
     */
    public get validFeedbackText(): string
    {
        return this._validFeedbackElement.innerText;
    }

    /**
     * Set the valid feedback text
     */
    public set validFeedbackText(value: string)
    {
        this._validFeedbackElement.innerText = value;
    }

    /**
     * Get the invalid feedback text
     */
    public get invalidFeedbackText(): string
    {
        return this._invalidFeedbackElement.innerText;
    }

    /**
     * Set the invalid feedback text
     */
    public set invalidFeedbackText(value: string)
    {
        this._invalidFeedbackElement.innerText = value;
    }


    /**
     * Clear the input feedback classes, or set it None
     */
    private clearInputFeedbackClasses()
    {
        this.inputElement.classList.remove(InputFeedback.IsInvalidText, InputFeedback.IsValidText);
    }
}


export class InputFeedbackText extends InputFeedback
{
    public inputElement: HTMLInputElement;
}

export class InputFeedbackTextArea extends InputFeedback
{
    public inputElement: HTMLTextAreaElement;

    constructor(container: Element, autoClear: boolean = false)
    {
        super(container, autoClear);
    }
}


export class InputFeedbackSelect extends InputFeedback
{
    public inputElement: HTMLSelectElement;

    constructor(container: Element, autoClear: boolean = false)
    {
        super(container, autoClear);
    }
}
