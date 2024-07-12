import { PageUtility } from "../../utility/page-utility";
import { HomePageController } from "./home-page-controller";


PageUtility.pageReady(() =>
{
    const controller = new HomePageController();
    controller.control();
});