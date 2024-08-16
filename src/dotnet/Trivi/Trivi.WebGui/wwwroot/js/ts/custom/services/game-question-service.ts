import { ApiGameQuestions } from "../api/api-game-questions";
import { GameApiResponse } from "../domain/models/game-models";
import { GameQuestionApiResponse } from "../domain/models/question-models";
import { QuestionId } from "../domain/types/aliases";
import { ServiceUtility } from "../utility/service-utility";




export class GameQuestionService
{
    private _gameId: string;

    constructor(gameId: string)
    {
        this._gameId = gameId;
    }


    public async closeQuestion(questionId: QuestionId)
    {
        const api = new ApiGameQuestions(this._gameId);

        const response = await api.post(questionId);

        return await ServiceUtility.toServiceResponse<GameApiResponse>(response);
    }
}