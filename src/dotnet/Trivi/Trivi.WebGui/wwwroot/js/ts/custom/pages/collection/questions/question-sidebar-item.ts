import { Selector } from "../../../domain/helpers/element-selector/selector";

const elements = {
    containerClass: '.question-list-item',
    questionIdAttr: 'data-question-id',
    promptClass: '.question-list-item-prompt',
}

export class QuestionSidebarItem
{
    private _selector: Selector;
    private _container: HTMLButtonElement;
    private _prompt: HTMLDivElement;

    public get questionId()
    {
        const id = this._container.getAttribute(elements.questionIdAttr);

        if (!id)
        {
            throw new Error(`Element does not contain question id attribute.`);
        }

        return id;
    }


    public get isActive(): boolean
    {
        return this._container.classList.contains('active');
    }

    public set isActive(value: boolean)
    {
        if (value)
        {
            this._container.classList.add('active');
        }
        else
        {
            this._container.classList.remove('active');
        }
    }

    public get promptText(): string
    {
        return this._prompt.innerText;
    }

    public set promptText(value: string)
    {
        this._prompt.innerText = value;
    }


    constructor(e: Element)
    {
        this._selector = Selector.fromClosest<HTMLButtonElement>(elements.containerClass, e);
        this._container = this._selector.element as HTMLButtonElement;
        this._prompt = this._selector.querySelector<HTMLDivElement>(elements.promptClass);
    }

    public remove()
    {
        this._container.remove();
    }

    public static fromMouseEvent(e: MouseEvent)
    {
        const target = e.target as Element;

        if (!target) return null;

        if (target.closest(elements.containerClass))
        {
            return new QuestionSidebarItem(target);
        }

        return null;
    }
}


export const QuestionSidebarItemElements = elements;