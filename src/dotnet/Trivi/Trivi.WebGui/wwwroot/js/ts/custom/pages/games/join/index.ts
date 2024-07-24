import { PageUtility } from "../../../utility/page-utility";
import { JoinGamePageController } from "./join-game-page-controller";



PageUtility.pageReady(() =>
{
    const controller = new JoinGamePageController();
    controller.control();
});