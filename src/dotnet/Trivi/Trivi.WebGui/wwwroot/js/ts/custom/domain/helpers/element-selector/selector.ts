


export class Selector
{
    public readonly element: Element;

    constructor(container?: Element)
    {
        this.element = container ?? document.body;
    }

    public querySelector<T extends Element>(selector: string): T
    {
        const element = this.element.querySelector<T>(selector);

        if (!element)
        {
            throw new Error(`No element found within container (${this.element.classList.toString()}) with selector: ${selector}`);
        }
        return element;
    }

    public closest<T extends Element>(selector: string): T
    {
        const element = this.element.closest<T>(selector);

        if (!element)
        {
            throw new Error(`No element found within container with selector: ${selector}`);
        }
        return element;
    }



    public static fromClosest<T extends Element>(selector: string, child: Element): Selector
    {
        const element = child.closest<T>(selector);

        if (!element)
        {
            throw new Error(`No element found within container with selector: ${selector}`);
        }

        return new Selector(element);
        
    }

    public static fromString(selector: string): Selector
    {
        const element = document.querySelector(selector);

        if (!element)
        {
            throw new Error(`No element found within container with selector: ${selector}`);
        }

        return new Selector(element);
    }

    


    public static querySelector<T extends Element>(selector: string): T
    {
        const element = document.querySelector<T>(selector);

        if (!element)
        {
            throw new Error(`No element found within container with selector: ${selector}`);
        }

        return element;
    }

}


export const DocumentSelector = new Selector(document.body);



