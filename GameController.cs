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
    private readonly HighScoreService _highScoreService;

    public GameController(IInputOutput ioService, HighScoreService highScoreService)
    {
        _ioService = ioService;
        _highScoreService = highScoreService;
    }

    public void StartGame()
    {
        _ioService.WriteLine("Enter your user name:\n");
        string name = _ioService.ReadLine();
        bool playOn = true;

        while (playOn)
        {
            string goal = MakeGoal();
            PlaySingleGame(name, goal);
            playOn = QueryContinue();
        }
    }

    private void PlaySingleGame(string playerName, string goal)
    {
        _ioService.WriteLine("New game:\n");
        //comment out or remove next line to play real games!
        _ioService.WriteLine("For practice, number is: " + goal + "\n");  // Optional debug line

        int nGuess = 0;
        while (true)
        {
            string guess = GetValidGuess();
            nGuess++;
            string result = CheckBullsAndCows(goal, guess);
            _ioService.WriteLine(result + "\n");

            if (result == "BBBB,")
            {
                _highScoreService.RecordResult(playerName, nGuess);
                _highScoreService.ShowTopList(_ioService);
                _ioService.WriteLine("Correct, it took " + nGuess + " guesses\n");
                break;
            }
        }
    }

    private bool QueryContinue()
    {
        _ioService.WriteLine("Continue? (y/n)");
        string answer = _ioService.ReadLine();
        return answer != null && answer.Trim().ToLower() == "y"; 
    }

    private string GetValidGuess()
    {
        while (true)
        {
            _ioService.WriteLine("Enter your guess (4 digits):");
            string guess = _ioService.ReadLine().Trim();

            if (guess.Length == 4)
            {
                return guess;
            }
            _ioService.WriteLine("Invalid input. Please enter exactly 4 digits.");
        }
    }

    static string MakeGoal() // icke static för senare testning?
    {
        Random randomGenerator = new Random();
        var digits = Enumerable.Range(0, 10).ToList();
        var shuffledDigits = digits.OrderBy(d => randomGenerator.Next()).Take(4).ToList();

        return string.Join("", shuffledDigits);
    }

    public string CheckBullsAndCows(string goal, string guess) // vill nog sen dela upp den i två för SoP
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
        return $"{new String('B', bulls)},{new String('C', cows)}";
    }

}