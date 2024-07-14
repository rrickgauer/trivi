import { ApiCollections } from "../api/api-collections";
import { CollectionApiResponse, PostCollectionApiRequestBody } from "../domain/models/collection-models";
import { Guid } from "../domain/types/aliases";
import { ServiceUtility } from "../utility/service-utility";



export class CollectionsService
{
    public async createCollection(data: PostCollectionApiRequestBody)
    {
        const api = new ApiCollections();

        const apiresponse = await api.post(data);

        return await ServiceUtility.toServiceResponse<CollectionApiResponse>(apiresponse);
    }

    public async updateCollection(collectionId: Guid, data: PostCollectionApiRequestBody)
    {
        const api = new ApiCollections();

        const apiresponse = await api.put(collectionId, data);

        return await ServiceUtility.toServiceResponse<CollectionApiResponse>(apiresponse);
    }

    public async deleteCollection(collectionId: Guid)
    {
        const api = new ApiCollections();

        const apiresponse = await api.delete(collectionId);

        return await ServiceUtility.toServiceResponseNoContent(apiresponse);
    }
}