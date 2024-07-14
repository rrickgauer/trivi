import { QuestionType } from "../../enums/question-type";
import { PutMultipleChoiceApiRequest, PutQuestionApiRequest } from "../../models/question-models";
import { NewQuestion } from "./new-question";


export class NewMultipleChoice extends NewQuestion<PutMultipleChoiceApiRequest>
{

    public getQuestionType = () => QuestionType.MultipleChoice;
    protected readonly _defaultPrompt = "New multiple choice";

    public toForm(): PutQuestionApiRequest
    {
        const result: PutMultipleChoiceApiRequest = {
            collectionId: this._collectionId,
            prompt: this._defaultPrompt,
        };

        return result;
    }

}
