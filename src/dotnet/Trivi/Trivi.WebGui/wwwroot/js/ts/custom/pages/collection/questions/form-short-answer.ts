import { NativeEvents } from "../../../domain/constants/native-events";
import { InputFeedbackText } from "../../../domain/helpers/input-feedback";
import { ServiceResponse } from "../../../domain/models/api-response";
import { PutShortAnswerApiRequest, ShortAnswerApiResponse } from "../../../domain/models/question-models";
import { Guid } from "../../../domain/types/aliases";
import { QuestionForm } from "./form-question";

const elements = {
    editorContainerClass: ".question-editor-sa",
    answerInputId: "#form-question-sa-input-answer",
}


export class FormShortAnswer extends QuestionForm<ShortAnswerApiResponse>
{
    private readonly _answer: InputFeedbackText;
    protected _currentQuestion: ShortAnswerApiResponse;

    private get _answerValue(): string | null
    {
        return this._answer.inputElement.value ?? null;
    }

    private set _answerValue(value: string)
    {
        this._answer.inputElement.value = value;
    }


    constructor(collectionId: Guid)
    {
        super({
            collectionId: collectionId,
            editorContainerSelector: elements.editorContainerClass,
        });


        this._answer = new InputFeedbackText(this._selector.querySelector<HTMLInputElement>(elements.answerInputId));
    }

    public showQuestion(question: ShortAnswerApiResponse)
    {
        this.clearAlertsContainer();

        this._currentQuestion = question;

        this._promptValue = question.prompt;
        this._answerValue = question.correctAnswer;
    }

    public control()
    {
        super.control();
        this.addListeners();
    }

    private addListeners = () =>
    {
        this._answer.inputElement.addEventListener(NativeEvents.KeyUp, (e) =>
        {
            this.setSubmitButtonDisabled();
        });
    }

    protected onPromptInputKeyUp()
    {
        this.setSubmitButtonDisabled();
    }

    protected setSubmitButtonDisabled()
    {
        if (this.getPromptValueLength() === 0 || ((this._answerValue?.length) ?? 0) === 0)
        {
            this._btnSubmit.button.disabled = true;
        }
        else
        {
            this._btnSubmit.button.disabled = false;
        }
    }


    protected async sendPutRequest(): Promise<ServiceResponse<ShortAnswerApiResponse> | null>
    {
        const requestData = this.getPutRequestData();

        if (!requestData)
        {
            return null;
        }

        return await this._questionService.saveShortAnswer(this._currentQuestion.id, requestData);
    }


    private getPutRequestData(): PutShortAnswerApiRequest | null
    {
        const requestData = this.getBasicPutRequestData<PutShortAnswerApiRequest>();

        if (!this._answerValue)
        {
            return null;
        }

        requestData.correctAnswer = this._answerValue;

        return requestData;
    }


}


export const FormShortAnswerElements = elements;


