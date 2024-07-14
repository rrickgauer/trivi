import { QuestionType } from "../../enums/question-type";
import { PutShortAnswerApiRequest } from "../../models/question-models";
import { NewQuestion } from "./new-question";


export class NewShortAnswer extends NewQuestion<PutShortAnswerApiRequest>
{
    public readonly getQuestionType = () => QuestionType.ShortAnswer;
    protected readonly _defaultPrompt = "New short answer";

    public toForm()
    {
        const result: PutShortAnswerApiRequest = {
            collectionId: this._collectionId,
            correctAnswer: "None",
            prompt: this._defaultPrompt,
        };

        return result;
    }
}
