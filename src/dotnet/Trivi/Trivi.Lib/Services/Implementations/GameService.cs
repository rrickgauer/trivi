﻿using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Runtime.Versioning;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Constants;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Repository.Contracts;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Services.Implementations;

[AutoInject<IGameService>(AutoInjectionType.Scoped, InjectionProject.Always)]
public class GameService(IGameRepository gameRepository, ITableMapperService tableMapperService, IGameQuestionService gameQuestionService) : IGameService
{
    private readonly IGameRepository _gameRepository = gameRepository;
    private readonly ITableMapperService _tableMapperService = tableMapperService;
    private readonly IGameQuestionService _gameQuestionService = gameQuestionService;

    public async Task<ServiceResponse<List<ViewGame>>> GetUserGamesAsync(Guid userId)
    {
        try
        {
            var table = await _gameRepository.SelectUserGamesAsync(userId);
            return _tableMapperService.ToModels<ViewGame>(table);
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }

    public async Task<ServiceResponse<ViewGame>> GetGameAsync(string gameId)
    {
        try
        {
            ServiceResponse<ViewGame> result = new();

            var row = await _gameRepository.SelectGameAsync(gameId);

            if (row != null)
            {
                result.Data = _tableMapperService.ToModel<ViewGame>(row);
            }

            return result;
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }


    public async Task<ServiceResponse<ViewGame>> CreateGameAsync(Game game)
    {
        var validateResult = ValidateNewGame(game);

        if (!validateResult.Successful)
        {
            return new(validateResult.Errors);
        }

        try
        {
            await _gameRepository.InsertGameAsync(game);
        }
        catch (RepositoryException ex)
        {
            return ex;
        }

        return await GetGameAsync(game.Id!);
    }

    private ServiceResponse ValidateNewGame(Game game)
    {
        ServiceResponse result = new();

        if (game.QuestionTimeLimit is ushort timeLimit)
        {
            if (timeLimit < GameConstants.MinQuestionTimeLimit || timeLimit > GameConstants.MaxQuestionTimeLimit)
            {
                result.Errors.Add(ErrorCode.GamesInvalidQuestionTimeLimit);
            }
        }

        return result;
    }

    public async Task<ServiceResponse<ViewGame>> StartGameAsync(string gameId)
    {
        try
        {
            await _gameRepository.UpdateGameStatusAsync(gameId, GameStatus.Running);
        }
        catch (RepositoryException ex)
        {
            return ex;
        }


        var getQuestions = await _gameQuestionService.CreateGameQuestionsAsync(gameId);

        if (!getQuestions.Successful)
        {
            return new(getQuestions.Errors);
        }

        var questions = getQuestions.Data?.ToList() ?? new();

        var getGame = await GetGameAsync(gameId);

        if (!getGame.Successful)
        {
            return getGame;
        }

        if (getGame.Data is not null)
        {
            getGame.Data.Questions = questions;
        }

        return getGame;
    }


    public async Task<ServiceResponse<ViewGame>> ActivateNextGameQuestionAsync(string gameId)
    {
        try
        {
            await _gameRepository.ActivateNextGameQuestionAsync(gameId);
        }
        catch(RepositoryException ex)
        {
            return ex;
        }

        return await GetGameAsync(gameId);
    }
}
