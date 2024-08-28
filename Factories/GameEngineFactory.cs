using CodeSmells.Classes;
using CodeSmells.GameEngines;
using CodeSmells.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.Factories;

public class GameEngineFactory : IGameEngineFactory
{
    private INumberGenerator _numberGenerator;

    public GameEngineFactory() 
    {
        _numberGenerator = new RandomNumberGenerator();
    }

    public IGameEngine CreateGameEngine(string choice)
    {
        switch (choice)
        {
            case "1":
                return new BullsAndCowsGameEngine(_numberGenerator);
            case "2":
                return new WsAndLsGameEngine();
            case "3":
                return new MastermindGameEngine();
            default:
                return new BullsAndCowsGameEngine(_numberGenerator);
        }
    }
}
