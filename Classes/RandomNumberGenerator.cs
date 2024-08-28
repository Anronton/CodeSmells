using CodeSmells.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.Classes;

public class RandomNumberGenerator : INumberGenerator
{
    private Random _random = new Random();

    public int[] GenerateNumbers(int count, int min, int max)
    {
        var possibleNumbers = Enumerable.Range(min, max).ToList();
        var shuffledNumbers = possibleNumbers.OrderBy(x => _random.Next()).ToList();

        return shuffledNumbers.Take(count).ToArray();
    }
}
