import { PageUtility } from "../../utility/page-utility";
import { CollectionsPageController } from "./collections-page-controller";


PageUtility.pageReady(() =>
{
    const controller = new CollectionsPageController();
    controller.control();
})