import { ApiQuestionResponses } from "../api/api-responses";
import { ResponseApiResponse, ResponseShortAnswerApiPostRequest } from "../domain/models/question-response-models";
import { QuestionId } from "../domain/types/aliases";
import { ServiceUtility } from "../utility/service-utility";



export class QuestionResponseService
{
    public async createShortAnswerResponse(questionId: QuestionId, data: ResponseShortAnswerApiPostRequest)
    {
        const api = new ApiQuestionResponses(questionId);

        const result = await api.post(data);

        return await ServiceUtility.toServiceResponse<ResponseApiResponse>(result);
    }


}