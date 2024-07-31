import { HttpMethods } from "../domain/constants/api-constants";
import { ApplicationTypes } from "../domain/constants/application-types";
import { ResponseApiPostRequestTypes, ResponseShortAnswerApiPostRequest } from "../domain/models/question-response-models";
import { QuestionId } from "../domain/types/aliases";
import { ApiEndpoints } from "./api-base";



export class ApiQuestionResponses
{
    private readonly _questionId: string;
    private readonly _url: string;

    constructor(questionId: QuestionId)
    {
        this._questionId = questionId;
        this._url = `${ApiEndpoints.Responses}/${this._questionId}`;
    }


    public async post(data: ResponseApiPostRequestTypes)
    {
        const url = this._url;

        return await fetch(url, {
            body: JSON.stringify(data),
            headers: ApplicationTypes.GetJsonHeaders(),
            method: HttpMethods.POST,
        });
    }

}


