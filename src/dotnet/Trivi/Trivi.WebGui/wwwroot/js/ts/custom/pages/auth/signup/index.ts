import { PageUtility } from "../../../utility/page-utility";
import { SignupPageController } from "./signup-page-controller";


PageUtility.pageReady(() =>
{
    const controller = new SignupPageController();
    controller.control();
});