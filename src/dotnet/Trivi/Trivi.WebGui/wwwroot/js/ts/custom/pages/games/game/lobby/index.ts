import { PageUtility } from "../../../../utility/page-utility";
import { UrlUtility } from "../../../../utility/url-utility";
import { GameLobbyPageController } from "./game-lobby-page-controller";



PageUtility.pageReady(async () =>
{
    const controller = new GameLobbyPageController({
        gameId: UrlUtility.getCurrentPathValue(1)!,
        playerId: UrlUtility.getQueryParmValue('player')!
    });

    await controller.control();
})