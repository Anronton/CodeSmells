using CodeSmells.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.Services;

public class InputOutputService : IInputOutput
{
    public string ReadLine()
    {
        return Console.ReadLine().Trim();
    }
    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }
}
