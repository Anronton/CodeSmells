using CodeSmells.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.Interfaces;

public interface IHighScore
{
    void RecordResult(string playerName, int nGuess);
    public void ShowTopList(IInputOutput io); 
    
}
