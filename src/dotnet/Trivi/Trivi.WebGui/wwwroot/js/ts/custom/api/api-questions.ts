import { HttpMethods } from "../domain/constants/api-constants";
import { ApplicationTypes } from "../domain/constants/application-types";
import { Guid, QuestionId } from "../domain/types/aliases";
import { MappingUtility } from "../utility/mapping-utility";
import { UrlUtility } from "../utility/url-utility";
import { ApiEndpoints } from "./api-base";



export class ApiQuestions
{
    private readonly _url: string;

    constructor()
    {
        this._url = ApiEndpoints.Questions;
    }

    public async getAll(collectionId: Guid)
    {
        const queryParms = UrlUtility.getQueryParmsString({
            collection: collectionId,
        });
        
        const url = `${this._url}?${queryParms}`;

        return await fetch(url);
    }

    public async get(questionId: QuestionId)
    {
        const url = `${this._url}/${questionId}`;

        return await fetch(url);
    }

    public async put(questionId: string, formData: object)
    {
        const url = `${this._url}/${questionId}`;

        return await fetch(url, {
            body: MappingUtility.toJson(formData),
            method: HttpMethods.PUT,
            headers: ApplicationTypes.GetJsonHeaders(),
        });
    }

    public async delete(questionId: QuestionId)
    {
        const url = `${this._url}/${questionId}`;

        return await fetch(url, {
            method: HttpMethods.DELETE,
        });
    }
}