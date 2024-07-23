import { ApiQuestions } from "../api/api-questions";
import { INewQuestion } from "../domain/helpers/new-questions/new-question";
import { ServiceResponse } from "../domain/models/api-response";
import { GetQuestionsApiResponse, MultipleChoiceApiResponse, PutMultipleChoiceApiRequest, PutShortAnswerApiRequest, PutTrueFalseApiRequest, QuestionApiResponse, ShortAnswerApiResponse, TrueFalseAnswerApiResponse } from "../domain/models/question-models";
import { Guid, QuestionId } from "../domain/types/aliases";
import { ServiceUtility } from "../utility/service-utility";



export class QuestionsService
{
    public async getQuestions(collectionId: Guid)
    {
        const api = new ApiQuestions();

        const apiResponse = await api.getAll(collectionId);

        return await ServiceUtility.toServiceResponse<GetQuestionsApiResponse>(apiResponse);
    }

    public async createNew<TApiResponse extends QuestionApiResponse>(question: INewQuestion)
    {
        const api = new ApiQuestions();

        const response = await api.put(question.questionId, question.toForm());

        return await ServiceUtility.toServiceResponse<TApiResponse>(response);
    }


    public async saveMultipleChoice(questionId: QuestionId, data: PutMultipleChoiceApiRequest): Promise<ServiceResponse<MultipleChoiceApiResponse>>
    {
        return await this.saveQuestion<MultipleChoiceApiResponse>(questionId, data);
    }

    public async saveShortAnswer(questionId: QuestionId, data: PutShortAnswerApiRequest): Promise<ServiceResponse<ShortAnswerApiResponse>>
    {
        return await this.saveQuestion<ShortAnswerApiResponse>(questionId, data);
    }

    public async saveTrueFalse(questionId: QuestionId, data: PutTrueFalseApiRequest): Promise<ServiceResponse<TrueFalseAnswerApiResponse>>
    {
        return await this.saveQuestion<TrueFalseAnswerApiResponse>(questionId, data);
    }

    private async saveQuestion<TResponse extends QuestionApiResponse>(questionId: QuestionId, data: object)
    {
        const api = new ApiQuestions();

        const response = await api.put(questionId, data);

        return await ServiceUtility.toServiceResponse<TResponse>(response);
    }
    

    public async getQuestion(questionId: QuestionId)
    {
        return await this.fetchQuestion<QuestionApiResponse>(questionId);
    }

    public async getShortAnswer(questionId: QuestionId)
    {
        return await this.fetchQuestion<ShortAnswerApiResponse>(questionId);
    }

    public async getTrueFalse(questionId: QuestionId)
    {
        return await this.fetchQuestion<TrueFalseAnswerApiResponse>(questionId);
    }

    public async getMultipleChoice(questionId: QuestionId)
    {
        return await this.fetchQuestion<MultipleChoiceApiResponse>(questionId);
    }

    private async fetchQuestion<T extends QuestionApiResponse>(questionId: QuestionId): Promise<ServiceResponse<T>>
    {
        const api = new ApiQuestions();

        const apiResponse = await api.get(questionId);

        return await ServiceUtility.toServiceResponse<T>(apiResponse);
    }


    public async deleteQuestion(questionId: QuestionId)
    {
        const api = new ApiQuestions();

        const response = await api.delete(questionId);

        return await ServiceUtility.toServiceResponseNoContent(response);
    }

}