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
        //ioService.WriteLine("For practice, number is: " + currentGoal + Environment.NewLine); // Debug line
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

    public string CheckGuess(string guess)
    {
        throw new NotImplementedException();
    }

    public string GetValidGuess(IInputOutput ioService)
    {
        throw new NotImplementedException();
    }

    

    public bool IsGameWon(string guess)
    {
        throw new NotImplementedException();
    }

    public bool QueryContinue(IInputOutput ioService)
    {
        throw new NotImplementedException();
    }
}
