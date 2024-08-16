import { Toast } from "bootstrap";
import { ToastBase, ToastData, ToastError, ToastStandard, ToastSuccess } from "../domain/helpers/toasts/toasts";
import { ToastHtmlTemplate } from "../templates/toast-template";


export class ToastUtility
{
    public static readonly Container = document.querySelector('#toasts-wrapper .toast-container') as HTMLDivElement;

    public static showError = (data: ToastData) =>
    {
        const toast = new ToastError(data);
        return ToastUtility.show(toast);
    }

    public static showSuccess = (data: ToastData) =>
    {
        const toast = new ToastSuccess(data);
        return ToastUtility.show(toast);
    }

    public static showStandard = (data: ToastData) =>
    {
        const toast = new ToastStandard(data);
        return ToastUtility.show(toast);
    }

    public static show = (toast: ToastBase) =>
    {
        const template = new ToastHtmlTemplate();

        const html = template.toHtml(toast);

        ToastUtility.Container.insertAdjacentHTML("afterbegin", html);

        const element = document.querySelector(`.toast[data-js-toast-id="${toast.id}"]`);

        Toast.getOrCreateInstance(element!).show();

        return toast;

    }
}