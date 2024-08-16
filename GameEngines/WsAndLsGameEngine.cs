using CodeSmells.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.GameEngines;

public class WsAndLsGameEngine : IGameEngine
{
    private string currentGoal;
    private int numberOfGuesses;
    public int NumberOfGuesses => numberOfGuesses;

    public void InitializeGame(IInputOutput ioService)
    {
        currentGoal = MakeGoal();
        numberOfGuesses = 0;
        ioService.WriteLine("For practice, number is: " + currentGoal + Environment.NewLine); // Debug line
    }

    public string GetValidGuess(IInputOutput ioService) // eventuellt typsäkra så att den bara tar emot int
    {
        while (true)
        {
            ioService.WriteLine("Enter your guess (4 digits):");
            string guess = ioService.ReadLine().Trim();

            if (guess.Length == 4)
            {
                return guess;
            }
            ioService.WriteLine("Invalid input. Please enter exactly 4 digits.");
        }
    }

    public string CheckGuess(string guess)
    {
        numberOfGuesses++;
        return CheckBullsAndCows(currentGoal, guess);
    }

    public bool QueryContinue(IInputOutput ioService)
    {
        ioService.WriteLine("Continue? (y/n)");
        string answer = ioService.ReadLine();
        return answer != null && answer.Trim().ToLower() == "y";
    }

    private string MakeGoal()
    {
        Random randomGenerator = new Random();
        var digits = Enumerable.Range(0, 10).ToList();
        var shuffledDigits = digits.OrderBy(d => randomGenerator.Next()).Take(4).ToList();

        return string.Join("", shuffledDigits);
    }

    public string CheckBullsAndCows(string goal, string guess) // se kanske över senare för potentiell ytterligare SoP
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
                bullMarked[i] = true;  // Markera denna position som en tjur
            }
        }

        // Nu räkna kor, ignorera tjur-markeringar
        for (int i = 0; i < Math.Min(4, goal.Length); i++)
        {
            if (!bullMarked[i])  // Söker kor bara om inte redan tjur
            {
                for (int j = 0; j < Math.Min(4, guess.Length); j++)
                {
                    if (i != j && goal[i] == guess[j] && !bullMarked[j] && !cowMarked[j])
                    {
                        cows++;
                        cowMarked[j] = true;  // Markera denna position för att undvika dubbelräkning av samma ko
                        break;  // Bryt loopen när en matchning hittas
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


