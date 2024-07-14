import { IController } from "../../../domain/contracts/icontroller";
import { Guid } from "../../../domain/types/aliases";



export class CollectionQuestionsPageController implements IController
{
    private readonly _collectionId: string;

    constructor(collectionId: Guid)
    {
        this._collectionId = collectionId;
    }

    public control()
    {
        
    }
}