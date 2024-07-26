import { PageUtility } from "../../../../utility/page-utility";
import { UrlUtility } from "../../../../utility/url-utility";
import { AdminLobbyPageController } from "./admin-lobby-page-controller";



PageUtility.pageReady(async () =>
{
    const gameId = UrlUtility.getCurrentPathValue(2);

    const controller = new AdminLobbyPageController(gameId!);

    await controller.control();
})