using CodeSmells.Classes;
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
    private readonly IHighScore _highScoreService;
    private readonly IGameEngine _gameEngine;

    public GameController(IInputOutput ioService, IHighScore highScoreService, IGameEngine gameEngine)
    {
        _ioService = ioService;
        _highScoreService = highScoreService;
        _gameEngine = gameEngine;
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

                if (result == "BBBB," || result == "WWWW,") // placholder lösning
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