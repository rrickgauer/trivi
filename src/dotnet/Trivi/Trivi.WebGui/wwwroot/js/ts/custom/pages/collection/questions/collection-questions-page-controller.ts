import { IControllerAsync } from "../../../domain/contracts/icontroller";
import { DeleteQuestionButtonClickedData, DeleteQuestionButtonClickedEvent, OpenQuestionData, OpenQuestionEvent } from "../../../domain/events/events";
import { GetQuestionsApiResponse } from "../../../domain/models/question-models";
import { Guid, QuestionId } from "../../../domain/types/aliases";
import { QuestionsService } from "../../../services/questions-service";
import { ErrorUtility } from "../../../utility/error-utility";
import { MessageBoxUtility } from "../../../utility/message-box-utility";
import { PageLoadingUtility } from "../../../utility/page-loading-utility";
import { UrlUtility } from "../../../utility/url-utility";
import { QuestionFormsController } from "./forms-controller";
import { QuestionsSidebarController } from "./questions-sidebar-controller";




export class CollectionQuestionsPageController implements IControllerAsync
{
    private readonly _collectionId: string;
    private readonly _questionsService: QuestionsService;
    private readonly _questionsSidebar: QuestionsSidebarController;
    private readonly _formsController: QuestionFormsController;

    private readonly _initialQuestion: QuestionId | null;

    constructor(args: { collectionId: Guid, initialQuestion: QuestionId | null })
    {
        this._collectionId = args.collectionId;
        this._questionsService = new QuestionsService();
        this._questionsSidebar = new QuestionsSidebarController(this._collectionId);
        this._formsController = new QuestionFormsController(this._collectionId);

        this._initialQuestion = args.initialQuestion;
    }

    public async control()
    {
        const getQuestions = await this.fetchQuestions();

        if (!getQuestions)
        {
            return;
        }

        this._questionsSidebar.control(getQuestions);

        this._formsController.control();

        this.addListeners();

        // load initial question if provided in the url
        await this.openInitialQuestion();

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
        OpenQuestionEvent.addListener(async (message) =>
        {
            await this.onOpenQuestionEvent(message.data!);
        });

        DeleteQuestionButtonClickedEvent.addListener(async (message) =>
        {
            await this.onDeleteQuestionButtonClickedEvent(message.data!);
        });
    }


    private async onOpenQuestionEvent(message: OpenQuestionData)
    {
        UrlUtility.setQueryParmQuiet('question', message.questionId);
        await this._formsController.editQuestion(message.questionId);
    }

    private async openInitialQuestion()
    {
        if (!this._initialQuestion)
        {
            return;
        }


        await this._formsController.editQuestion(this._initialQuestion);
        this._questionsSidebar.activateQuestion(this._initialQuestion);
    }


    private async onDeleteQuestionButtonClickedEvent(message: DeleteQuestionButtonClickedData)
    {
        // remove sidebar question item
        this._questionsSidebar.removeQuestion(message.questionId);

        // hide the current question form
        this._formsController.hideCurrentQuestion();

        // remove the question id from the url
        UrlUtility.removeQueryParmQuiet('question');

    }
}