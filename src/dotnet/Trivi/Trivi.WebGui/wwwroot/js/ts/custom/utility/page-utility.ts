


export class PageUtility
{
    public static pageReady(fn)
    {
        if (document.readyState !== 'loading')
        {
            fn();
        }
        else
        {
            document.addEventListener('DOMContentLoaded', fn);
        }
    }

    public static refreshPage()
    {
        window.location.href = window.location.href;
    }
}