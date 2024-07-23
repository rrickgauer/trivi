import { AnswerApiResponse } from "../domain/models/answer-models";
import { HtmlTemplate } from "./html-template";



export class AnswerTemplate extends HtmlTemplate<AnswerApiResponse>
{
    public toHtml(model: AnswerApiResponse): string
    {
        const dataIdAttr = `data-answer-id="${model.id}"`;

        const checked = model.isCorrect ? "checked" : "";

        let html = `

        <li class="answers-list-item my-3" ${dataIdAttr} >
            <div class="d-flex align-items-center">
                <input class="form-check-input answers-list-item-checkbox me-2 mt-0" type="checkbox" title="Is Correct" ${checked}>
                <input type="text" class="form-control form-control-sm answers-list-item-answer-input" placeholder="Answer" required value="${model.answer}">
                <button type="button" class="btn btn-sm btn-reset btn-delete-answer" title="Remove answer"><i class='bx bx-x'></i></button>
            </div>
        </li>
        `;

        return html;
    }

}