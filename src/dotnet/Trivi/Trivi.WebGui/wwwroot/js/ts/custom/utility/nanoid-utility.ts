

import { customAlphabet } from "../../../node_modules/nanoid/index.browser";
import { QuestionType } from "../domain/enums/question-type";
import { NanoID } from "../domain/types/aliases";

export class NanoIdUtility
{
    private static readonly nanoIdEngine = customAlphabet("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");

    private static readonly ID_LENGTH = 17;

    public static new(): NanoID
    {
        const nanoid = this.nanoIdEngine(this.ID_LENGTH);

        return nanoid;
    }

    public static newQuestionId(questionType: QuestionType): string
    {
        const nanoid = this.new();

        return `${questionType}_${nanoid}`;
    }
}