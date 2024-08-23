



export class ClipboardUtility
{
    public static copyToClipboard(text: string)
    {
        navigator.clipboard.writeText(text);
    }
}
