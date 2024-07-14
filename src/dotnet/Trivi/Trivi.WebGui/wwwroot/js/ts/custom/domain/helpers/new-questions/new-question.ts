import { DateTime } from "../../../lib/luxon";
import { NanoIdUtility } from "../../../utility/nanoid-utility";
import { QuestionType } from "../../enums/question-type";
import { PutQuestionApiRequest, QuestionApiResponse } from "../../models/question-models";
import { Guid } from "../../types/aliases";


export interface INewQuestion
{
    toForm(): object;
    questionId: string;
}


export abstract class NewQuestion<TApiRequest extends PutQuestionApiRequest> implements INewQuestion
{
    public abstract toForm(): TApiRequest;
    
    protected abstract _defaultPrompt: string;
    public abstract getQuestionType(): QuestionType;

    protected readonly _id = NanoIdUtility.new();

    protected readonly _collectionId: string;
    

    public get questionId()
    {
        return `${this.getQuestionType()}_${this._id}`;
    }

    constructor(collectionId: Guid)
    {
        this._collectionId = collectionId;

    }


    public toResponse(): QuestionApiResponse
    {
        const result: QuestionApiResponse = {
            questionCollectionId: this._collectionId,
            questionCreatedOn: DateTime.now().toSQL(),
            questionId: this.questionId,
            questionPrompt: this._defaultPrompt,
            questionType: this.getQuestionType(),
        }

        return result;
    }
}

