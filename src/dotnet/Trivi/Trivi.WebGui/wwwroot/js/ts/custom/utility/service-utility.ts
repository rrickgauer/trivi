import { HttpStatusCode } from "../domain/enums/http-status-code";
import { ApiResponse, ApiResponseNoContent, ServiceResponse, ValidationErrorsApiResponse } from "../domain/models/api-response";
import { ApiForbiddenException, ApiNotFoundException, ApiValidationException } from "../domain/models/exceptions";




export class ServiceUtility
{
    public static async toServiceResponseNoContent(response: Response): Promise<ServiceResponse<any>>
    {
        if (response.ok || response.status === HttpStatusCode.BadRequest)
        {
            const apiResponse = new ApiResponseNoContent(response);
            return await apiResponse.toServiceResponse();
        }
        else if (response.status === HttpStatusCode.UnprocessableEntity)
        {
            await ServiceUtility.handleUnprocessableEntityApiResponse(response);
        }
        else if (response.status === HttpStatusCode.NotFound)
        {
            ServiceUtility.handleNotFoundApiResponse(response);
        }

        const responseText = await response.text();
        throw new Error(responseText);
    }


    public static async toServiceResponse<T>(response: Response): Promise<ServiceResponse<T>> 
    {
        if (response.ok || response.status === HttpStatusCode.BadRequest)
        {
            const data: ApiResponse<T> = await response.json();
            const result = new ServiceResponse<T>(data, response.ok);

            return result;
        }
        else if (response.status === HttpStatusCode.UnprocessableEntity)
        {
            await ServiceUtility.handleUnprocessableEntityApiResponse(response);
            
        }
        else if (response.status === HttpStatusCode.NotFound)
        {
            ServiceUtility.handleNotFoundApiResponse(response);
            
        }
        else if (response.status === HttpStatusCode.Forbidden)
        {
            ServiceUtility.handleForbiddenApiResponse(response);
        }

        const responseText = await response.text();
        throw new Error(responseText);
    }

    private static handleUnprocessableEntityApiResponse = async (response: Response) =>
    {
        const validationErrors: ValidationErrorsApiResponse = await response.json();
        throw new ApiValidationException(validationErrors);
    }

    private static handleNotFoundApiResponse = (response: Response) =>
    {
        throw new ApiNotFoundException();
    }

    private static handleForbiddenApiResponse(response: Response)
    {
        throw new ApiForbiddenException();
    }

}







