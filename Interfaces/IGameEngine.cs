using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.Interfaces;

public interface IGameEngine
{
    void InitializeGame(IInputOutput ioService);
    string GetValidGuess(IInputOutput ioService);
    string CheckGuess(string guess);
    bool QueryContinue(IInputOutput ioService);
    int NumberOfGuesses { get; }
    //bool isGameWon(string guess);

}
