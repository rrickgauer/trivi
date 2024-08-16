import { GuidUtility } from "../../../utility/guid-utility";





export enum ToastType
{
    STANDARD,
    SUCCESS,
    ERROR,

}


export type ToastData = {
    message: string,
    title?: string,
}


export abstract class ToastBase
{
    public readonly id: string = GuidUtility.getRandomGuid();

    public title: string;
    public message: string;

    public abstract toastType: ToastType;

    constructor(message: string, title: string)
    {
        this.message = message;
        this.title = title;
    }
}

export class ToastStandard extends ToastBase
{
    public readonly toastType = ToastType.STANDARD;

    constructor(data: ToastData)
    {
        super(data.message, data.title ?? "Message");
    }
}



export class ToastSuccess extends ToastBase
{
    public readonly toastType = ToastType.SUCCESS;

    constructor(data: ToastData)
    {
        super(data.message, data.title ?? "Success");
    }
}



export class ToastError extends ToastBase
{
    public readonly toastType = ToastType.ERROR;

    constructor(data: ToastData)
    {
        super(data.message, data.title ?? "Error");
    }
}
