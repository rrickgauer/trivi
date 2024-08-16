import { PageUtility } from "../../../../../utility/page-utility";
import { GameQuestionPageController } from "../game-question-page-controller";
import { MultipleChoiceGameQuestionPageController } from "./multiple-choice-game-question-page-controller";


PageUtility.pageReady(() =>
{
    const gameParms = GameQuestionPageController.getGameQuestionUrlParms();
    const controller = new MultipleChoiceGameQuestionPageController(gameParms);
    controller.control();
});
