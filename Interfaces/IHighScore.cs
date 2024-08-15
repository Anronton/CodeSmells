using CodeSmells.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.Interfaces;

public interface IHighScore
{
    void RecordResult(string playerName, int totalGuesses);
    public void ShowTopList(IInputOutput io); 
    
}
