using CodeSmells.Classes;
using CodeSmells.Interfaces;
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
    private readonly IInputOutput _io;
    public GameController(IInputOutput ioService)
    {
        _io = ioService;
    }

    public void StartGame()
    {
        _io.WriteLine("Enter your user name:\n");
        string name = _io.ReadLine();
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
        _io.WriteLine("New game:\n");
        //comment out or remove next line to play real games!
        //_io.WriteLine("For practice, number is: " + goal + "\n");  // Optional debug line

        int nGuess = 0;
        while (true)
        {
            string guess = GetValidGuess();
            nGuess++;
            string result = CheckBullsAndCows(goal, guess);
            _io.WriteLine(result + "\n");

            if (result == "BBBB,")
            {
                RecordResult(playerName, nGuess);
                ShowTopList();
                _io.WriteLine("Correct, it took " + nGuess + " guesses\n");
                break;
            }
        }
    }

    private bool QueryContinue()
    {
        _io.WriteLine("Continue? (y/n)");
        string answer = _io.ReadLine();
        return answer != null && answer.Trim().ToLower() == "y"; 
    }


    private void RecordResult(string playerName, int nGuess)
    {
        using (StreamWriter output = new StreamWriter("result.txt", append: true))
        {
            output.WriteLine($"{playerName}#&#{nGuess}");
        }
    }

    private string GetValidGuess()
    {
        while (true)
        {
            _io.WriteLine("Enter your guess (4 digits):");
            string guess = _io.ReadLine().Trim();

            if (guess.Length == 4)
            {
                return guess;
            }
            _io.WriteLine("Invalid input. Please enter exactly 4 digits.");
        }
    }

    static string MakeGoal() // icke static?
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


    private void ShowTopList() // denna hör nog till playerData i högsta grad
    {
        StreamReader input = new StreamReader("result.txt");
        List<PlayerData> results = new List<PlayerData>();
        string line;
        while ((line = input.ReadLine()) != null)
        {
            string[] nameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
            string name = nameAndScore[0];
            int guesses = Convert.ToInt32(nameAndScore[1]);
            PlayerData pd = new PlayerData(name, guesses);
            int pos = results.IndexOf(pd);
            if (pos < 0)
            {
                results.Add(pd);
            }
            else
            {
                results[pos].Update(guesses);
            }


        }
        results.Sort((p1, p2) => p1.Average().CompareTo(p2.Average()));
        _io.WriteLine("Player   games average");
        foreach (PlayerData p in results)
        {
            _io.WriteLine(string.Format("{0,-9}{1,5:D}{2,9:F2}", p.Name, p.NGames, p.Average()));
        }
        input.Close();
    }
}