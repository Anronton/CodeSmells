using CodeSmells.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.GameEngines;

public class MastermindGameEngine : IGameEngine
{
    private string currentGoal;
    private int numberOfGuesses;
    public int NumberOfGuesses => numberOfGuesses;

    public void InitializeGame(IInputOutput ioService)
    {
        currentGoal = MakeGoal();
        numberOfGuesses = 0;
        ioService.WriteLine("For practice, number is: " + currentGoal + Environment.NewLine);
        ioService.WriteLine("Enter your guess (4 colors). Colors shorthand: R, G, B, Y, O or P ");
    }
    private string MakeGoal()
    {
        Random randomGenerator = new Random();
        var colors = new List<string> { "R", "G", "B", "Y", "O", "P" }; // står för: Red, Green, Blue, Yellow, Orange och Purple
        var goal = new StringBuilder();

        for (int i = 0; i < 4; i++) 
        {
            int colorIndex = randomGenerator.Next(colors.Count);
            goal.Append(colors[colorIndex]);
        }

        return goal.ToString();
    }

    public string GetValidGuess(IInputOutput ioService)
    {
        HashSet<char> validColors = new HashSet<char> { 'R', 'G', 'B', 'Y', 'O', 'P' }; // vi anger ett set av tillåtna tecken
        while (true)
        {
            string guess = ioService.ReadLine().Trim().ToUpper();

            if (guess.Length == 4 && guess.All(c => validColors.Contains(c))) // som vi testar emot här
            {
                return guess;
            }
            ioService.WriteLine("Invalid input. Please enter exactly 4 valid colors.");
        }
    }

    public string CheckGuess(string guess)
    {
        numberOfGuesses++;
        return CheckBlackAndWhiteFlags(currentGoal, guess);
    }

    public string CheckBlackAndWhiteFlags(string goal, string guess)
    {
        int blackFlags = 0;
        int whiteFlags = 0;
        bool[] blackMarked = new bool[goal.Length];
        bool[] whiteMarked = new bool[goal.Length];

        //identifiera alla svarta flaggor, dvs rätt färg på rätt plats
        for (int i = 0; i < goal.Length; i++)
        {
            if (goal[i] == guess[i])
            {
                blackFlags++;
                blackMarked[i] = true;
            }
        }

        //identifiera alla vita flaggor, dvs rätt färg på fel plats
        for (int i = 0; i < goal.Length; i++)
        {
            if (!blackMarked[i])
            {
                for (int j = 0; j < guess.Length; j++)
                {
                    if (!blackMarked[j] && !whiteMarked[j] && goal[i] == guess[j])
                    {
                        whiteFlags++;
                        whiteMarked[j] = true;
                        break;
                    }
                }
            }
        }

        return FormatBlackAndWhiteFlags(blackFlags, whiteFlags);
    }

    private string FormatBlackAndWhiteFlags(int blackFlags, int whiteFlags)
    {
        return $"{new string('B', blackFlags)},{new string('W', whiteFlags)}";
    }

    public bool IsGameWon(string guess)
    {
        return CheckBlackAndWhiteFlags(currentGoal, guess) == "BBBB,";
    }

    public bool QueryContinue(IInputOutput ioService)
    {
        ioService.WriteLine("Continue? (y/n) or [Press ENTER to continue]");
        string answer = ioService.ReadLine().Trim().ToLower();
        return string.IsNullOrEmpty(answer) || answer == "y";
    }
}
