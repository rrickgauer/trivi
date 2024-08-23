using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Services.Implementations;

[AutoInject<IGradingService>(AutoInjectionType.Scoped, InjectionProject.Always)]
public class GradingService(IQuestionService questionService, IAnswerService answerService) : IGradingService
{
    #region - Private Members -
    
    private readonly IQuestionService _questionService = questionService;
    private readonly IAnswerService _answerService = answerService;

    #endregion

    #region - Public Methods -

    /// <summary>
    /// Grade the given true false response
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    public async Task<ServiceResponse<ResponseGrade>> GradeResponseAsync(ResponseTrueFalse response)
    {
        try
        {
            var question = await GetTrueFalseAsync(response.QuestionId!);

            if (question.CorrectAnswer == response.AnswerGiven)
            {
                return ResponseGrade.Correct(question);
            }
            else
            {
                return ResponseGrade.Incorrect();
            }
        }
        catch (ServiceException ex)
        {
            return new(ex.Errors);
        }
    }


    /// <summary>
    /// Grade the given multiple choice response
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    public async Task<ServiceResponse<ResponseGrade>> GradeResponseAsync(ResponseMultipleChoice response)
    {
        try
        {
            // get the question
            var question = await GetMultipleChoiceAsync(response.QuestionId!);

            // get the question's answers
            var answers = await GetMultipleChoiceAnswersAsync(response.QuestionId!);
            var answer = answers.FirstOrDefault(a => a.Id == response.AnswerGiven);

            if (answer is null)
            {
                return ResponseGrade.Incorrect();
            }

            if (!answer.IsCorrect)
            {
                return ResponseGrade.Incorrect();
            }

            return ResponseGrade.Correct(question);
        }
        catch (ServiceException ex)
        {
            return new(ex.Errors);
        }
    }

    /// <summary>
    /// Grade the given short answer response
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    public async Task<ServiceResponse<ResponseGrade>> GradeResponseAsync(ResponseShortAnswer response)
    {
        try
        {
            var question = await GetShortAnswerAsync(response.QuestionId!);

            string correctAnswer = StripShortAnswer(question.CorrectAnswer);
            string givenAnswer = StripShortAnswer(response.AnswerGiven);

            if (correctAnswer.Equals(givenAnswer, StringComparison.OrdinalIgnoreCase))
            {
                return ResponseGrade.Correct(question);
            }

            return ResponseGrade.Incorrect();

        }
        catch(ServiceException ex)
        {
            return new(ex.Errors);
        }
    }

    #endregion

    #region - Private Methods -

    private async Task<ViewTrueFalse> GetTrueFalseAsync(QuestionId questionId)
    {
        var getQuestion = await _questionService.GetTrueFalseAsync(questionId);

        return getQuestion.GetData();
    }



    private async Task<ViewShortAnswer> GetShortAnswerAsync(QuestionId questionId)
    {
        var getQuestion = await _questionService.GetShortAnswerAsync(questionId);

        return getQuestion.GetData();
    }


    private async Task<ViewMultipleChoice> GetMultipleChoiceAsync(QuestionId questionId)
    {
        var getQuestion = await _questionService.GetMultipleChoiceAsync(questionId);

        return getQuestion.GetData();
    }

    private async Task<List<ViewAnswer>> GetMultipleChoiceAnswersAsync(QuestionId questionId)
    {
        var getAnswers = await _answerService.GetAnswersAsync(questionId);

        return getAnswers.GetData();
    }


    private static string StripShortAnswer(string? text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return string.Empty;
        }

        return text.Replace(" ", "").ToLower().Trim();
    }

    #endregion
}






