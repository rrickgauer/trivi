import { QuestionType } from "../domain/enums/question-type";
import { NotImplementedException } from "../domain/models/exceptions";
import { QuestionApiResponse } from "../domain/models/question-models";
import { HtmlTemplate } from "./html-template";



export class QuestionSidebarListItemTemplate extends HtmlTemplate<QuestionApiResponse>
{
    public toHtml(model: QuestionApiResponse): string 
    {
        const questionIdAttr = `data-question-id="${model.id}"`;
        const questionType = this.getQuestionTypeDisplayText(model.questionType);

        let html = `
            <button type="button" class="list-group-item list-group-item-action question-list-item" ${questionIdAttr}>
                <div class="text-truncate fw-bolder fs-5 question-list-item-prompt">
                    ${model.prompt}
                </div>
                <div>${questionType}</div>
            </button>
        `;

        return html;
    }

    private getQuestionTypeDisplayText(questionType: QuestionType)
    {
        switch (questionType)
        {
            case QuestionType.MultipleChoice:
                return "Multiple Choice";
            case QuestionType.ShortAnswer:
                return "Short Answer";
            case QuestionType.TrueFalse:
                return "True False";
            default:
                throw new NotImplementedException();
        }
    }

}