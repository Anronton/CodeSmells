using CodeSmells.Classes;
using CodeSmells.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.GameEngines;

public class BullsAndCowsGameEngine : IGameEngine
{
    private string currentGoal;
    private int numberOfGuesses;
    public int NumberOfGuesses => numberOfGuesses;

    private bool _gameWon;
    public bool GameWon => _gameWon;
    private INumberGenerator _numberGenerator;

    public BullsAndCowsGameEngine(INumberGenerator numberGenerator)
    {
        _numberGenerator = numberGenerator;
    }

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
        if(result == "BBBB,")
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
        var digits = _numberGenerator.GenerateNumbers(4, 0, 10); 
        return string.Join("", digits);
    }

    public string CheckBullsAndCows(string goal, string guess) 
    {
        int bulls = 0;
        int cows = 0;
        bool[] bullMarked = new bool[goal.Length];
        bool[] cowMarked = new bool[goal.Length];

        // Först räkna och markera tjurar
        for (int i = 0; i < Math.Min(4, goal.Length); i++)
        {
            if (goal[i] == guess[i])
            {
                bulls++;
                bullMarked[i] = true;
            }
        }

        // Nu räkna och markera kor, ignorera tjur-markeringar
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
        return $"{new string('B', bulls)},{new string('C', cows)}";
    }
}
