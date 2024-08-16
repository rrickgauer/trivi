import { PageUtility } from "../../../../../utility/page-utility";
import { GameQuestionPageController } from "../game-question-page-controller";
import { TrueFalseGameQuestionPageController } from "./true-false-game-question-page-controller";


PageUtility.pageReady(() =>
{
    const gameParms = GameQuestionPageController.getGameQuestionUrlParms();
    const controller = new TrueFalseGameQuestionPageController(gameParms);
    controller.control();
});