import { NativeEvents } from "../../constants/native-events";
import { Selector } from "../element-selector/selector";


const elements = {
    containerClass: '.ranger',
    inputClass: '.ranger-input',
    valueClass: '.ranger-value',
    displayClass: '.ranger-display',

    dataPrefixAttr: 'data-ranger-prefix',
}

export class Ranger
{
    private _container: HTMLDivElement;
    private _selector: Selector;
    private _input: HTMLInputElement;
    private _display: HTMLDivElement;

    public get value(): number
    {
        const value = this._input.value;
        return parseFloat(value);
    }

    public set value(value: number)
    {
        if (value < this.min || value > this.max)
        {
            throw new RangeError(`Argument is not between ${this.min}-${this.max}`);
        }

        this._input.value = value.toString();
    }


    public get min(): number
    {
        return parseFloat(this._input.min);
    }

    public set min(value: number)
    {
        this._input.min = value.toString();
    }

    public get max(): number
    {
        return parseFloat(this._input.max);
    }

    public set max(value: number)
    {
        this._input.max = value.toString();
    }

    public get step(): number
    {
        return parseFloat(this._input.step);
    }

    public set step(value: number)
    {
        this._input.step = value.toString();
    }


    public get prefix(): string | null
    {
        return this._container.getAttribute(elements.dataPrefixAttr);
    }


    constructor(e: Element | string)
    {
        if (e instanceof Element)
        {
            this._selector = Selector.fromClosest(elements.containerClass, e);
        }
        else
        {
            this._selector = Selector.fromString(e);
        }


        this._container = this._selector.element as HTMLDivElement;
        this._input = this._selector.querySelector<HTMLInputElement>(elements.inputClass);
        this._display = this._selector.querySelector<HTMLDivElement>(elements.displayClass);

        this.updateValueDisplay();
        this.addListeners();
    }


    private addListeners = () =>
    {
        this._input.addEventListener(NativeEvents.Change, (e) =>
        {
            this.updateValueDisplay();
        });
    }


    private updateValueDisplay()
    {
        const prefix = this.prefix ?? 'Value: ';
        let html = `${prefix}${this.value}`;

        this._display.innerHTML = html;
        
    }


    public static initFromContainer(e: Element)
    {
        const element = e.querySelector(elements.containerClass);

        if (!element)
        {
            throw new Error(`No ranger component found within the given element.`);
        }

        return new Ranger(element);
    }
}



export const RangerElements = elements;
