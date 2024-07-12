import { Selector } from "./element-selector/selector";


export class SelectInput<T>
{
    public readonly selectElement: HTMLSelectElement;
    private _selector: Selector;

    public get selectedValue(): T
    {
        return this.selectElement.selectedOptions[0].value as T;
    }

    public set selectedValue(value: T)
    {
        const option = this._selector.querySelector<HTMLOptionElement>(`option[value="${value}"]`);
        option.selected = true;
    }

    constructor(selectElement: HTMLSelectElement)
    {
        this._selector = new Selector(selectElement);
        this.selectElement = this._selector.element as HTMLSelectElement;
    }
}