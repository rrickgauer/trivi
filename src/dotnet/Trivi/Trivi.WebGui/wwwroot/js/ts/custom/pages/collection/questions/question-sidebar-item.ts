import { Selector } from "../../../domain/helpers/element-selector/selector";

const elements = {
    containerClass: '.question-list-item',
    questionIdAttr: 'data-question-id',
}

export class QuestionSidebarItem
{
    private _selector: Selector;
    private _container: HTMLButtonElement;

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


    constructor(e: Element)
    {
        this._selector = Selector.fromClosest<HTMLButtonElement>(elements.containerClass, e);
        this._container = this._selector.element as HTMLButtonElement;
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