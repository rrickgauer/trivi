import { IControllerAsync } from "../../../domain/contracts/icontroller";
import { OpenQuestionData, OpenQuestionEvent } from "../../../domain/events/events";
import { GetQuestionsApiResponse } from "../../../domain/models/question-models";
import { Guid } from "../../../domain/types/aliases";
import { QuestionsService } from "../../../services/questions-service";
import { ErrorUtility } from "../../../utility/error-utility";
import { MessageBoxUtility } from "../../../utility/message-box-utility";
import { PageLoadingUtility } from "../../../utility/page-loading-utility";
import { QuestionsSidebarController } from "./questions-sidebar-controller";


export class CollectionQuestionsPageController implements IControllerAsync
{
    private readonly _collectionId: string;
    private _questionsService: QuestionsService;
    private _questionsSidebar: QuestionsSidebarController;

    constructor(collectionId: Guid)
    {
        this._collectionId = collectionId;
        this._questionsService = new QuestionsService();
        this._questionsSidebar = new QuestionsSidebarController(this._collectionId);
    }

    public async control()
    {
        const getQuestions = await this.fetchQuestions();

        if (!getQuestions)
        {
            return;
        }

        this._questionsSidebar.control(getQuestions);

        this.addListeners();

        PageLoadingUtility.hideLoader();
    }

    private async fetchQuestions(): Promise<GetQuestionsApiResponse | null>
    {
        try
        {
            const response = await this._questionsService.getQuestions(this._collectionId);

            if (!response.successful)
            {
                MessageBoxUtility.showErrorList(response.response.errors);
                return null;
            }

            return response.response.data;
        }
        catch (error)
        {
            ErrorUtility.onException(error, {
                onApiNotFoundException: (e) => MessageBoxUtility.showError({ message: 'Collection not found. Try reloading the page.' }),
                onApiForbiddenException: (e) => MessageBoxUtility.showError({ message: 'You do not have permission to view this collection.' }),
                onOther: (e) =>
                {
                    MessageBoxUtility.showError({
                        message: 'Unexpected error. Please try again later.',
                    });
                },
            });

            console.error(error);

            return null;
        }
    }


    private addListeners = () =>
    {
        OpenQuestionEvent.addListener((message) =>
        {
            this.onOpenQuestionEvent(message.data!);
        });
    }


    private onOpenQuestionEvent(message: OpenQuestionData)
    {
        //alert(message.questionId);
    }
}