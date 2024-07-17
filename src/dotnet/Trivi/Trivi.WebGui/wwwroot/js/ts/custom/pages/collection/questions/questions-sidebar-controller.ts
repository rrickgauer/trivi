import { NativeEvents } from "../../../domain/constants/native-events";
import { IControllerArgs } from "../../../domain/contracts/icontroller";
import { QuestionType } from "../../../domain/enums/question-type";
import { OpenQuestionEvent, QuestionUpdatedData, QuestionUpdatedEvent } from "../../../domain/events/events";
import { Selector } from "../../../domain/helpers/element-selector/selector";
import { NewMultipleChoice } from "../../../domain/helpers/new-questions/multiple-choice";
import { NewQuestion } from "../../../domain/helpers/new-questions/new-question";
import { NewShortAnswer } from "../../../domain/helpers/new-questions/short-answer";
import { NewTrueFalse } from "../../../domain/helpers/new-questions/true-false";
import { GetQuestionsApiResponse, PutQuestionApiRequest, QuestionApiResponse } from "../../../domain/models/question-models";
import { Guid, QuestionId } from "../../../domain/types/aliases";
import { QuestionsService } from "../../../services/questions-service";
import { QuestionSidebarListItemTemplate } from "../../../templates/question-sidebar-item-template";
import { QuestionSidebarItemElements, QuestionSidebarItem } from "./question-sidebar-item";


const elements = {
    containerClass: '.questions-sidebar-left-container',
    listContainerClass: '.questions-list',
    newQuestionTypeButtonClass: '.new-question-dropdown-item',
    newQuestionDropdownTypeAttr: 'data-new-question-type',
}


export class QuestionsSidebarController implements IControllerArgs<GetQuestionsApiResponse>
{
    private _collectionId: string;
    private _selector: Selector;
    private _container: HTMLDivElement;
    private _listContainer: HTMLDivElement;
    private _questionsService: QuestionsService;
    private _template: QuestionSidebarListItemTemplate;

    constructor(collectionId: Guid)
    {
        this._collectionId = collectionId;
        this._questionsService = new QuestionsService();

        this._template = new QuestionSidebarListItemTemplate();

        this._selector = Selector.fromString(elements.containerClass);
        this._container = this._selector.element as HTMLDivElement;
        this._listContainer = this._selector.querySelector<HTMLDivElement>(elements.listContainerClass);
    }

    public control(apiResponse: GetQuestionsApiResponse)
    {
        this.renderQuestions(apiResponse.questions);
        this.addListeners();
    }

    public activateQuestion(questionId: QuestionId)
    {
        const question = this.getQuestion(questionId);

        if (!question)
        {
            return;
        }

        // activate the question
        this.deactivateAllQuestions();
        question.isActive = true;
    }

    public removeQuestion(questionId: QuestionId)
    {
        const question = this.getQuestion(questionId);
        question?.remove();
    }

    private renderQuestions(questions: QuestionApiResponse[])
    {
        this._listContainer.innerHTML = this._template.toHtmls(questions);
    }

    private addListeners = () =>
    {
        // listen for sidebar question click
        this._container.addEventListener(NativeEvents.Click, (e) =>
        {
            const sidebarItem = QuestionSidebarItem.fromMouseEvent(e);

            if (sidebarItem)
            {
                this.openQuestion(sidebarItem);
            }
        });


        // listen for new question dropdown button click
        this._container.querySelectorAll<HTMLButtonElement>(elements.newQuestionTypeButtonClass).forEach(button =>
        {
            button.addEventListener(NativeEvents.Click, async (e) =>
            {
                const questionType = button.getAttribute(elements.newQuestionDropdownTypeAttr) as QuestionType;

                if (questionType)
                {
                    await this.createNewQuestion(questionType);
                }
            });
        });

        QuestionUpdatedEvent.addListener((message) =>
        {
            this.onQuestionUpdatedEvent(message.data!);
        });


    }

    // update the sidebar item when the user has updated the question in the form
    private onQuestionUpdatedEvent(message: QuestionUpdatedData)
    {
        const question = this.getQuestion(message.question.id);

        if (question)
        {
            question.promptText = message.question.prompt;
        }
    }

    private openQuestion(question: QuestionSidebarItem)
    {
        // activate the question
        this.deactivateAllQuestions();
        question.isActive = true;

        // let the editor know to open it
        OpenQuestionEvent.invoke(this, {
            questionId: question.questionId,
        });
    }

    private deactivateAllQuestions()
    {
        this.getAllQuestions().forEach(i => i.isActive = false);
    }

    private getQuestion(questionId: QuestionId): QuestionSidebarItem | null
    {
        const questions = this.getAllQuestions();

        const question = questions.find(q => q.questionId === questionId);

        return question ?? null;
    }

    private getAllQuestions(): QuestionSidebarItem[]
    {
        const result = [] as QuestionSidebarItem[];

        const elements = this._container.querySelectorAll<HTMLButtonElement>(QuestionSidebarItemElements.containerClass);

        elements.forEach(e =>
        {
            result.push(new QuestionSidebarItem(e));
        });

        return result;
    }


    private async createNewQuestion(questionType: QuestionType)
    {
        // save new question to api

        let newAnswer = this.getNewQuestionInstance(questionType);


        console.log({
            q: newAnswer,
            questionId: newAnswer.getQuestionType(),
        });


        this._questionsService.createNew(newAnswer);

        // generate html for it
        const fakeResponse = newAnswer.toResponse();
        const questionHtml = this._template.toHtml(fakeResponse);
        this._listContainer.innerHTML = questionHtml + this._listContainer.innerHTML;
    }


    private getNewQuestionInstance(questionType: QuestionType): NewQuestion<PutQuestionApiRequest>
    {
        switch (questionType)
        {
            case QuestionType.ShortAnswer:
                return new NewShortAnswer(this._collectionId);
                
            case QuestionType.MultipleChoice:
                return new NewMultipleChoice(this._collectionId);

            case QuestionType.TrueFalse:
                return new NewTrueFalse(this._collectionId);
                
            default:
                throw new Error("Not implemented");
        }
    }

}


