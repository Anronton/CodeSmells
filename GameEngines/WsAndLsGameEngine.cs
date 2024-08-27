using CodeSmells.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.GameEngines;

public class WsAndLsGameEngine : IGameEngine // Detta spel är egentligen bara en copy-paste av BullsAndCows
{                                            // vi hade den bara i tidigt skede för att testa ifall spelvalsfunktionen fungerade innan vi byggde MasterMind
    private string currentGoal;
    private int numberOfGuesses;
    public int NumberOfGuesses => numberOfGuesses;
    private bool _gameWon;
    public bool GameWon => _gameWon; // testar

    public void InitializeGame(IInputOutput ioService)
    {
        _gameWon = false;
        currentGoal = MakeGoal();
        numberOfGuesses = 0;
        ioService.WriteLine("For practice, number is: " + currentGoal + Environment.NewLine);
        ioService.WriteLine("Enter your guess (4 digits):");
    }

    public string GetValidGuess(IInputOutput ioService)
    {
        while (true)
        {
            string guess = ioService.ReadLine().Trim();

            if (guess.Length == 4 && guess.All(char.IsDigit))
            {
                return guess;
            }
            ioService.WriteLine("Invalid input. Please enter exactly 4 digits.");
        }
    }

    public string CheckGuess(string guess)
    {
        numberOfGuesses++;
        string result = CheckBullsAndCows(currentGoal, guess);
        if (result == "WWWW,")
        {
            _gameWon = true;
        }
        return result;
    }

    public bool IsGameWon(string guess)
    {
        return _gameWon;
    }

    public bool QueryContinue(IInputOutput ioService)
    {
        ioService.WriteLine("Continue? (y/n) or [Press ENTER to continue]");
        string answer = ioService.ReadLine().Trim().ToLower();
        return string.IsNullOrEmpty(answer) || answer == "y";
    }

    private string MakeGoal()
    {
        Random randomGenerator = new Random();
        var digits = Enumerable.Range(0, 10).ToList();
        var shuffledDigits = digits.OrderBy(d => randomGenerator.Next()).Take(4).ToList();

        return string.Join("", shuffledDigits);
    }

    public string CheckBullsAndCows(string goal, string guess)
    {
        int bulls = 0;
        int cows = 0;
        bool[] bullMarked = new bool[goal.Length];
        bool[] cowMarked = new bool[goal.Length];

        for (int i = 0; i < Math.Min(4, goal.Length); i++)
        {
            if (goal[i] == guess[i])
            {
                bulls++;
                bullMarked[i] = true;
            }
        }

        for (int i = 0; i < Math.Min(4, goal.Length); i++)
        {
            if (!bullMarked[i])
            {
                for (int j = 0; j < Math.Min(4, guess.Length); j++)
                {
                    if (i != j && goal[i] == guess[j] && !bullMarked[j] && !cowMarked[j])
                    {
                        cows++;
                        cowMarked[j] = true;
                        break;  
                    }
                }
            }
        }

        return FormatBullsAndCows(bulls, cows);
    }

    private string FormatBullsAndCows(int bulls, int cows)
    {
        return $"{new string('W', bulls)},{new string('L', cows)}";
    }
}


