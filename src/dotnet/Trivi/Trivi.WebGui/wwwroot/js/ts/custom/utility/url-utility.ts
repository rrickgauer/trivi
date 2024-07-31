

export class UrlUtility
{
    public static getCurrentPathValue = (index: number): string | null =>
    {
        const url = new URL(window.location.href);
        return UrlUtility.getPathValue(index, url);
    }

    public static getPathValue = (index: number, url: URL): string | null =>
    {
        const pathValues = url.pathname.split('/').filter(v => v !== "");

        if (index > pathValues.length)
        {
            return null;
        }

        return pathValues[index];
    }

    public static replacePath(newPath: string, url?: string | URL): URL
    {
        const result = new URL(url ?? window.location.href);

        result.pathname = newPath;

        return result;
    }


    public static toQueryParmsString(data: object): string
    {
        const urlParms = new URLSearchParams();

        for (const key in data)
        {
            urlParms.set(key, `${data[key]}`);
        }

        return urlParms.toString();
    }

    public static getQueryParmValueNumber(key: string): number | null
    {
        const stringValue = this.getQueryParmValue(key);

        if (stringValue)
        {
            return parseInt(stringValue);
        }
        else
        {
            return null;
        }

    }


    public static getQueryParmValue(key: string): string | null
    {
        const currentUrl = new URL(window.location.href);
        return currentUrl.searchParams.get(key);
    }

    public static getQueryParmValueStringTyped<T>(key: string): T | null
    {
        const currentUrl = new URL(window.location.href);
        return currentUrl.searchParams.get(key) as T;
    }

    public static setQueryParmQuiet(key: string, value: any)
    {
        const url = new URL(window.location.href);

        url.searchParams.set(key, `${value}`);

        window.history.pushState({}, '', url);
    }

    public static removeQueryParmQuiet(key: string)
    {
        const url = new URL(window.location.href);

        url.searchParams.delete(key);

        window.history.pushState({}, '', url);
    }



    public static createUrlFromSuffix(path: string)
    {
        const prefix = window.location.origin;
        const uri = `${prefix}${path}`;
        return new URL(uri);
    }
    
}