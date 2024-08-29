using CodeSmells.Classes;
using CodeSmells.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.Services;

public class HighScoreService : IHighScore
{
    private readonly string _resultFilePath;

    public HighScoreService(string resultFilePath)
    {
        _resultFilePath = resultFilePath;
    }

    public string GetResultFilePath() // används bara för testning av HighScoreServiceFactory, kanske inte bästa praxis
    {
        return _resultFilePath;
    }

    public void RecordResult(string playerName, int totalGuesses)
    {
        using (var output = new StreamWriter(_resultFilePath, append: true))
        {
            output.WriteLine($"{playerName}#&#{totalGuesses}");
        }
    }

    public void ShowTopList(IInputOutput io)
    {
        var results = ReadPlayerData();
        SortPlayerData(results);

        io.WriteLine("Player   games  average");
        foreach (var p in results)
        {
            io.WriteLine($"{p.Name,-9}{p.TotalGames,5:D}{p.Average(),9:F2}");
        }
    }
    // formattering: namn fältbredd på 9 tecken justerat till vänster, TotalGames 5heltal justerat till höger, flytande tal med 2decimaler totalt 9tecken ink decimaltecknet

    private List<PlayerData> ReadPlayerData()
    {
        var results = new List<PlayerData>();
        using (var input = new StreamReader(_resultFilePath))
        {
            string line;
            while ((line = input.ReadLine()) != null)
            {
                var nameAndGuesses = line.Split(new string[] { "#&#" }, StringSplitOptions.None); // en separator som säkerställer korrekt separering mellan namn och antal gissningar
                var name = nameAndGuesses[0]; //name är första delen
                var guesses = int.Parse(nameAndGuesses[1]); //gissningar är andra delen
                var playerData = new PlayerData(name, guesses);
                var pos = results.FindIndex(p => p.Name == name);
                if (pos == -1)
                {
                    results.Add(playerData);
                }
                else
                {
                    results[pos].Update(guesses);
                }
            }
        }
        return results;
    }

    private void SortPlayerData(List<PlayerData> results)
    {
        results.Sort((p1, p2) => p1.Average().CompareTo(p2.Average()));
    }
}
