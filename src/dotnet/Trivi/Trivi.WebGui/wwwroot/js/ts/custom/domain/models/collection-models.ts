import { DateTimeString, Guid } from "../types/aliases";


export type CollectionApiResponse = {
    id: Guid;
    name: string;
    userId: Guid;
    createdOn: DateTimeString;
    uriGui: string;
}


export type PostCollectionApiRequestBody = {
    name: string;
}

