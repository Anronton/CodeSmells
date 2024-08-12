using CodeSmells;
using CodeSmells.Interfaces;
using CodeSmells.Services;

namespace MooGame;

class Program
{

    public static void Main(string[] args)
    {
        IHighScore highScoreService = new HighScoreService();
        IInputOutput ioService = new InputOutputService();
        IGameEngine gameEngine = new BullsAndCowsGameEngine();
        GameController controller = new GameController(ioService, highScoreService, gameEngine);
        controller.StartGame();
    }
}

