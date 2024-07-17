import { AnswersService } from "../../../services/answers-service";
import { NanoIdUtility } from "../../../utility/nanoid-utility";
import { PageUtility } from "../../../utility/page-utility";
import { UrlUtility } from "../../../utility/url-utility";
import { CollectionQuestionsPageController } from "./collection-questions-page-controller";



PageUtility.pageReady(async () =>
{
    const collectionId = UrlUtility.getCurrentPathValue(2);

    if (!collectionId)
    {
        throw new Error(`Invalid collection ID: ${collectionId}`);
    }


    const controller = new CollectionQuestionsPageController(collectionId);
    await controller.control();
});

