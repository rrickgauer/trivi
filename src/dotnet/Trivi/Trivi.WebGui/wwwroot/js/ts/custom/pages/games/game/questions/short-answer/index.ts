import { PageUtility } from "../../../../../utility/page-utility";
import { GameQuestionPageController } from "../game-question-page-controller";
import { ShortAnswerGameQuestionPageController } from "./short-answer-game-question-page-controller";



PageUtility.pageReady(() =>
{
    const gameParms = GameQuestionPageController.getGameQuestionUrlParms();
    const controller = new ShortAnswerGameQuestionPageController(gameParms);
    controller.control();
});