import { PageUtility } from "../../../../utility/page-utility";
import { UrlUtility } from "../../../../utility/url-utility";
import { AdminQuestionPageController } from "./admin-question-page-controller";



PageUtility.pageReady(async () =>
{
    const controller = new AdminQuestionPageController({
        gameId: UrlUtility.getCurrentPathValue(2)!,
        questionId: UrlUtility.getCurrentPathValue(4)!,
    });

    await controller.control();
});