import { PageUtility } from "../../../utility/page-utility";
import { LoginPageController } from "./login-page-controller";



PageUtility.pageReady(() =>
{
    const controller = new LoginPageController();
    controller.control();
});


