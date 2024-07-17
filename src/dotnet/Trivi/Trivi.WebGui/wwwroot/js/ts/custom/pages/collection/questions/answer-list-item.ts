import { BootstrapUtilityClasses } from "../../../domain/constants/bootstrap-constants";
import { Selector } from "../../../domain/helpers/element-selector/selector";
import { SaveAnswerApiRequestBody } from "../../../domain/models/answer-models";



const elements = {
    containerClass: '.answers-list-item',
    dataIdAttr: 'data-answer-id',
    answerInputClass: '.answers-list-item-answer-input',
    checkboxInputClass: '.answers-list-item-checkbox',
    btnDeleteAnswerClass: '.btn-delete-answer',
}

export const AnswerListItemElements = elements;

export class AnswerListItem
{
    private _selector: Selector;
    private _container: HTMLLIElement;
    private _answerInput: HTMLInputElement;
    private _checkboxInput: HTMLInputElement;

    public get answerId(): string
    {
        const id = this._container.getAttribute(elements.dataIdAttr);

        if (!id)
        {
            throw new Error('Element does not contain valid answer id attribute.');
        }

        return id;
    }

    public set answerId(value: string)
    {
        this._container.setAttribute(elements.dataIdAttr, value);
    }

    public get answer(): string
    {
        return this._answerInput.value;
    }

    public set answer(value: string)
    {
        this._answerInput.value = value;
    }

    public get isChecked(): boolean
    {
        return this._checkboxInput.checked;
    }

    public set isChecked(value: boolean)
    {
        this._checkboxInput.checked = value;
    }


    constructor(e: Element)
    {
        this._selector = Selector.fromClosest(elements.containerClass, e);
        this._container = this._selector.element as HTMLLIElement;

        this._answerInput = this._selector.querySelector<HTMLInputElement>(elements.answerInputClass);
        this._checkboxInput = this._selector.querySelector<HTMLInputElement>(elements.checkboxInputClass);
    }

    public toSaveAnswerApiRequestBody(): SaveAnswerApiRequestBody
    {
        const result: SaveAnswerApiRequestBody = {
            answer: this.answer,
            isCorrect: this.isChecked,
        }

        return result;
    }

    public remove()
    {
        this._container.remove();
    }

    public hide()
    {
        this._container.classList.add(BootstrapUtilityClasses.DisplayNone);
    }

    public show()
    {
        this._container.classList.remove(BootstrapUtilityClasses.DisplayNone);
    }

    public static fromChild(e: Element): AnswerListItem | null
    {
        try
        {
            return new AnswerListItem(e);
        }
        catch (error)
        {
            return null;
        }
    }

    public static fromId(answerId: string): AnswerListItem | null
    {
        try
        {
            const element = document.querySelector<HTMLLIElement>(`${elements.containerClass}[${elements.dataIdAttr}="${answerId}"]`);

            if (!element)
            {
                return null;
            }

            return new AnswerListItem(element);
        }
        catch (error)
        {
            return null;
        }
    }
}