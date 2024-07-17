import { HttpMethods } from "../domain/constants/api-constants";
import { ApplicationTypes } from "../domain/constants/application-types";
import { SaveAnswerApiRequestBody } from "../domain/models/answer-models";
import { QuestionId } from "../domain/types/aliases";
import { ApiEndpoints } from "./api-base";



export class ApiAnswers
{
    private readonly _questionId: string;

    private get _url(): string
    {
        return `${ApiEndpoints.Questions}/${this._questionId}/answers`;
    }

    constructor(questionId: QuestionId)
    {
        this._questionId = questionId;
    }


    public async getAll(): Promise<Response>
    {
        const url = this._url;
        return await fetch(url);
    }

    public async get(answerId: string): Promise<Response>
    {
        const url = `${this._url}/${answerId}`;
        return await fetch(url);
    }

    public async delete(answerId: string): Promise<Response>
    {
        const url = `${this._url}/${answerId}`;

        return await fetch(url, {
            method: HttpMethods.DELETE,
        });
    }

    public async put(answerId: string, requestBody: SaveAnswerApiRequestBody): Promise<Response>
    {
        const url = `${this._url}/${answerId}`;

        return await fetch(url, {
            method: HttpMethods.PUT,
            body: JSON.stringify(requestBody),
            headers: ApplicationTypes.GetJsonHeaders(),
        });
    }

    public async putAll(requestBody: SaveAnswerApiRequestBody[]): Promise<Response>
    {
        const url = `${this._url}`;

        return await fetch(url, {
            method: HttpMethods.PUT,
            body: JSON.stringify(requestBody),
            headers: ApplicationTypes.GetJsonHeaders(),
        });
    }


}