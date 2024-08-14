import { PageUtility } from "../../../../utility/page-utility";
import { UrlUtility } from "../../../../utility/url-utility";
import { GameWaitingPageController } from "./game-waiting-page-controller";



PageUtility.pageReady(async () =>
{
    const controller = new GameWaitingPageController({
        gameId: UrlUtility.getCurrentPathValue(1)!,
        playerId: UrlUtility.getQueryParmValue('player')!
    });

    await controller.control();
});