using CodeSmells.Classes;
using CodeSmells.Factories;
using CodeSmells.Interfaces;
using CodeSmells.Services;
using MooGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells;

public class GameController
{
    private readonly IInputOutput _ioService;
    private IHighScore _highScoreService;
    private IGameEngine _gameEngine;

    private readonly IHighScoreServiceFactory _highScoreServiceFactory;
    private readonly IGameEngineFactory _gameEngineFactory;

    public GameController(IInputOutput ioService, IHighScoreServiceFactory highScoreServiceFactory, IGameEngineFactory gameEngineFactory)
    {
        _ioService = ioService;
        _highScoreServiceFactory = highScoreServiceFactory;
        _gameEngineFactory = gameEngineFactory;
    }

    public void ChooseGame()
    {
        _ioService.WriteLine("Choose a game:" + Environment.NewLine 
                           + "1. Bulls and Cows" + Environment.NewLine 
                           + "2. Wins and Losses" + Environment.NewLine 
                           + "3. Mastermind");
        string gameChoice = _ioService.ReadLine();

        _gameEngine = _gameEngineFactory.CreateGameEngine(gameChoice);
        _highScoreService = _highScoreServiceFactory.CreateHighScoreService(gameChoice);

        StartGame();
    }

    public void StartGame()
    {
        _ioService.WriteLine("Enter your user name:" + Environment.NewLine);
        string name = _ioService.ReadLine();
        bool playOn = true;


        while (playOn)
        {

            _gameEngine.InitializeGame(_ioService);

            while (true)
            {
                string guess = _gameEngine.GetValidGuess(_ioService);
                string result = _gameEngine.CheckGuess(guess);
                _ioService?.WriteLine(result + Environment.NewLine);

                if (_gameEngine.IsGameWon(guess))
                {
                    _highScoreService.RecordResult(name, _gameEngine.NumberOfGuesses);
                    _highScoreService.ShowTopList(_ioService);
                    _ioService.WriteLine("Correct, it took " + _gameEngine.NumberOfGuesses + " guesses" + Environment.NewLine);
                    playOn = _gameEngine.QueryContinue(_ioService);
                    break;
                }
            }
        }
    }
}