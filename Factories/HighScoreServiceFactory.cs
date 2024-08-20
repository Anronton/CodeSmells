using CodeSmells.Interfaces;
using CodeSmells.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.Factories;

public class HighScoreServiceFactory : IHighScoreServiceFactory
{
    public IHighScore CreateHighScoreService(string choice)
    {
        switch (choice)
        {
            case "1":
                return new HighScoreService("BullsAndCowScores.txt");
            case "2":
                return new HighScoreService("WsAndLsScores.txt");
            case "3":
                return new HighScoreService("MastermindScores.txt");
            default:
                return new HighScoreService("BullsAndCowScores.txt");
        }
    }
}
