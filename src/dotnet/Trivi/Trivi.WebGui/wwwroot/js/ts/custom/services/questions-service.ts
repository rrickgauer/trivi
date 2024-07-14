import { ApiQuestions } from "../api/api-questions";
import { INewQuestion } from "../domain/helpers/new-questions/new-question";
import { GetQuestionsApiResponse, QuestionApiResponse } from "../domain/models/question-models";
import { Guid } from "../domain/types/aliases";
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

}