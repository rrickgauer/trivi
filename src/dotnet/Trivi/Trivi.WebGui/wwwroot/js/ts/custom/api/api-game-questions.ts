import { HttpMethods } from "../domain/constants/api-constants";
import { QuestionId } from "../domain/types/aliases";
import { ApiEndpoints } from "./api-base";



export class ApiGameQuestions
{
    private readonly _gameId: string;
    private readonly _url: string;

    constructor(gameId: string)
    {
        this._gameId = gameId;
        this._url = `${ApiEndpoints.Games}/${this._gameId}`;
    }

    public async post(questionId: QuestionId)
    {
        const url = `${this._url}/questions/${questionId}/close`;

        return await fetch(url, {
            method: HttpMethods.POST,
        });
    }
}