import { PageUtility } from "../../../utility/page-utility";
import { UrlUtility } from "../../../utility/url-utility";
import { CollectionSetupPageController } from "./collection-setup-page-controller";



PageUtility.pageReady(() =>
{
    const collectionId = UrlUtility.getCurrentPathValue(2);

    if (!collectionId)
    {
        throw new Error(`Invalid collection ID`);
    }

    const controller = new CollectionSetupPageController(collectionId);
    controller.control();
});