import { ApiAnswers } from "../api/api-answers";
import { ApiQuestions } from "../api/api-questions";
import { AnswerApiResponse, SaveAnswerApiRequestBody } from "../domain/models/answer-models";
import { ServiceResponse } from "../domain/models/api-response";
import { QuestionId } from "../domain/types/aliases";
import { ServiceUtility } from "../utility/service-utility";



export class AnswersService
{
    private readonly _questionId: string;
    private readonly _api: ApiAnswers;

    constructor(questionId: QuestionId)
    {
        this._questionId = questionId;
        this._api = new ApiAnswers(this._questionId);
    }

    public async getAnswers(): Promise<ServiceResponse<AnswerApiResponse[]>>
    {
        const response = await this._api.getAll();

        return await ServiceUtility.toServiceResponse<AnswerApiResponse[]>(response);
    }

    public async getAnswer(answerId: string): Promise<ServiceResponse<AnswerApiResponse>>
    {
        const response = await this._api.get(answerId);

        return await ServiceUtility.toServiceResponse<AnswerApiResponse>(response);
    }

    public async deleteAnswer(answerId: string): Promise<ServiceResponse<any>>
    {
        const response = await this._api.delete(answerId);

        return await ServiceUtility.toServiceResponseNoContent(response);
    }

    public async saveAnswer(answerId: string, answerData: SaveAnswerApiRequestBody): Promise<ServiceResponse<AnswerApiResponse>>
    {
        const response = await this._api.put(answerId, answerData);

        return await ServiceUtility.toServiceResponse<AnswerApiResponse>(response);
    }

    public async replaceQuestionAnswers(answers: SaveAnswerApiRequestBody[]): Promise<ServiceResponse<AnswerApiResponse[]>>
    {
        const response = await this._api.putAll(answers);

        return await ServiceUtility.toServiceResponse<AnswerApiResponse[]>(response);
    }
}