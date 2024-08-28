using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.Interfaces;

public interface INumberGenerator
{
    int[] GenerateNumbers(int count, int min, int max);
}
