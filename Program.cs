using CodeSmells;
using CodeSmells.Factories;
using CodeSmells.GameEngines;
using CodeSmells.Interfaces;
using CodeSmells.Services;

namespace MooGame;

class Program
{
    public static void Main(string[] args)
    {
        IInputOutput ioService = new InputOutputService();
        IHighScoreServiceFactory highScoreServiceFactory = new HighScoreServiceFactory();
        IGameEngineFactory gameEngineFactory = new GameEngineFactory();
        
        GameController controller = new GameController(ioService, highScoreServiceFactory, gameEngineFactory);
        controller.ChooseGame();
    }
}

