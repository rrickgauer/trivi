import { HttpMethods } from "../domain/constants/api-constants";
import { ApplicationTypes } from "../domain/constants/application-types";
import { PostCollectionApiRequestBody } from "../domain/models/collection-models";
import { Guid } from "../domain/types/aliases";
import { MappingUtility } from "../utility/mapping-utility";
import { ApiEndpoints } from "./api-base";


export class ApiCollections {
    private readonly _url: string;

    constructor() {
        this._url = ApiEndpoints.Collections;
    }

    public async post(data: PostCollectionApiRequestBody) {
        const url = this._url;

        return await fetch(url, {
            method: HttpMethods.POST,
            headers: ApplicationTypes.GetJsonHeaders(),
            body: MappingUtility.toJson(data)
        });
    }

    public async put(collectionId: Guid, data: PostCollectionApiRequestBody)
    {
        const url = `${this._url}/${collectionId}`;

        return await fetch(url, {
            method: HttpMethods.PUT,
            headers: ApplicationTypes.GetJsonHeaders(),
            body: MappingUtility.toJson(data)
        });
    }

    public async delete(collectionId: Guid)
    {
        const url = `${this._url}/${collectionId}`;

        return await fetch(url, {
            method: HttpMethods.DELETE,
        });
    }
}
