import { QuestionType } from "../../enums/question-type";
import { PutTrueFalseApiRequest } from "../../models/question-models";
import { NewQuestion } from "./new-question";


export class NewTrueFalse extends NewQuestion<PutTrueFalseApiRequest>
{
    public readonly getQuestionType = () => QuestionType.TrueFalse;
    protected readonly _defaultPrompt = "New true false";

    public toForm(): PutTrueFalseApiRequest
    {
        return {
            collectionId: this._collectionId,
            correctAnswer: true,
            prompt: this._defaultPrompt,
        };
    }
}
