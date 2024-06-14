using CodeSmells.Classes;
using CodeSmells.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.Services;

public class HighScoreService // Denna och PlayerData-klassen ska granskas näst!
{
    private const string ResultFilePath = "result.txt"; // borde den vara i PascalCase?

    public void RecordResult(string playerName, int nGuess)
    {
        using (var output = new StreamWriter(ResultFilePath, append: true))
        {
            output.WriteLine($"{playerName}#&#{nGuess}");
        }
    }

    public void ShowTopList(IInputOutput io) // denna behöver jag förstå mer så att den går att förklara bättre, ska testa ifall den funkar
    {
        var results = new List<PlayerData>();
        using (var input = new StreamReader(ResultFilePath))
        {
            string line;
            while((line = input.ReadLine()) != null)
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

        results.Sort((p1, p2) => p1.Average().CompareTo(p2.Average()));
        io.WriteLine("Player   games  average");
        foreach (var p in results)
        {
            io.WriteLine($"{p.Name,-9}{p.NGames,5:D}{p.Average(),9:F2}"); // formattering: namn fältbredd på 9 tecken justerat till vänster, Ngames 5heltal justerat till höger, flytande tal med 2decimaler totalt 9tecken ink decimaltecknet
        }
    }



























    //public void ShowTopList() // den gamla för att motivera steg för steg sen om det behövs
    //{
    //    StreamReader input = new StreamReader("result.txt");
    //    List<PlayerData> results = new List<PlayerData>();
    //    string line;
    //    while ((line = input.ReadLine()) != null)
    //    {
    //        string[] nameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
    //        string name = nameAndScore[0];
    //        int guesses = Convert.ToInt32(nameAndScore[1]);
    //        PlayerData pd = new PlayerData(name, guesses);
    //        int pos = results.IndexOf(pd);
    //        if (pos < 0)
    //        {
    //            results.Add(pd);
    //        }
    //        else
    //        {
    //            results[pos].Update(guesses);
    //        }


    //    }
    //    results.Sort((p1, p2) => p1.Average().CompareTo(p2.Average()));
    //    _io.WriteLine("Player   games average");
    //    foreach (PlayerData p in results)
    //    {
    //        _io.WriteLine(string.Format("{0,-9}{1,5:D}{2,9:F2}", p.Name, p.NGames, p.Average()));
    //    }
    //    input.Close();
    //}
}
