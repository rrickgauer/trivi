

export interface IController
{
    control: () => void;
}

export interface IControllerAsync
{
    control: () => Promise<void>;
}

export interface IControllerArgs<T>
{
    control: (data: T) => void;
}