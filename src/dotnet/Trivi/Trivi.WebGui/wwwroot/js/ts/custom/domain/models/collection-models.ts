import { DateTimeString, Guid } from "../types/aliases";


export type CollectionApiResponse = {
    collectionId?: Guid;
    collectionName?: string;
    collectionUserId?: Guid;
    collectionCreatedOn?: DateTimeString;
    uriGui?: string;
}


export type PostCollectionApiRequestBody = {
    name: string;
}

