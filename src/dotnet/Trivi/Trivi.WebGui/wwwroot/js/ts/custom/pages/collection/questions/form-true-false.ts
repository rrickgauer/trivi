import { TrueFalseText } from "../../../domain/enums/true-false";
import { RadioGroup } from "../../../domain/helpers/radio-group/radio-group";
import { ServiceResponse } from "../../../domain/models/api-response";
import { PutTrueFalseApiRequest, TrueFalseAnswerApiResponse } from "../../../domain/models/question-models";
import { Guid } from "../../../domain/types/aliases";
import { QuestionForm } from "./form-question";


const elements = {
    editorContainerClass: '.question-editor-tf',
    answerRadioInputName: 'form-question-tf-input-answer',
    answerRadioInputTrueId: 'form-question-tf-input-answer-true',
    answerRadioInputFalseId: 'form-question-tf-input-answer-false',
}


export class FormTrueFalse extends QuestionForm<TrueFalseAnswerApiResponse>
{
    protected _currentQuestion: TrueFalseAnswerApiResponse;
    private _radioGroup: RadioGroup<TrueFalseText>;

    private get _answerValue(): boolean
    {
        return this._radioGroup.selectedValue === TrueFalseText.True;
    }

    private set _answerValue(value: boolean)
    {
        this._radioGroup.selectedValue = value ? TrueFalseText.True : TrueFalseText.False;
    }

    constructor(collectionId: Guid)
    {
        super({
            collectionId: collectionId,
            editorContainerSelector: elements.editorContainerClass,
        });


        this._radioGroup = new RadioGroup<TrueFalseText>(this._selector.querySelector<HTMLElement>('.radio-group'));
    }

    public showQuestion(question: TrueFalseAnswerApiResponse)
    {
        this.clearAlertsContainer();

        this._currentQuestion = question;
        this._promptValue = question.prompt;
        this._answerValue = question.correctAnswer;
    }

    public control()
    {
        super.control();
    }

    protected async sendPutRequest(): Promise<ServiceResponse<TrueFalseAnswerApiResponse> | null>
    {
        const requestData = this.getPutRequestData();

        if (!requestData)
        {
            return null;
        }

        return await this._questionService.saveTrueFalse(this._currentQuestion.id, requestData);
    }

    private getPutRequestData(): PutTrueFalseApiRequest | null
    {
        const requestData = this.getBasicPutRequestData<PutTrueFalseApiRequest>();
        requestData.correctAnswer = this._answerValue;

        return requestData;
    }
}


export const FormTrueFalseElements = elements;