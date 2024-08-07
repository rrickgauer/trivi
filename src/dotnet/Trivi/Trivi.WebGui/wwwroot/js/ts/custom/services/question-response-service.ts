import { ApiQuestionResponses } from "../api/api-question-responses";
import { ResponseShortAnswerApiPostRequest, ResponseTrueFalseApiPostRequest, ResponseShortAnswerApiResponse, ResponseTrueFalseApiResponse } from "../domain/models/question-response-models";
import { QuestionId } from "../domain/types/aliases";
import { ServiceUtility } from "../utility/service-utility";



export class QuestionResponseService
{
    public async createShortAnswerResponse(questionId: QuestionId, data: ResponseShortAnswerApiPostRequest)
    {
        const api = new ApiQuestionResponses(questionId);

        const result = await api.post(data);

        return await ServiceUtility.toServiceResponse<ResponseShortAnswerApiResponse>(result);
    }

    public async createTrueFalseResponse(questionId: QuestionId, data: ResponseTrueFalseApiPostRequest)
    {
        const api = new ApiQuestionResponses(questionId);

        const result = await api.post(data);

        return await ServiceUtility.toServiceResponse<ResponseTrueFalseApiResponse>(result);
    }


}