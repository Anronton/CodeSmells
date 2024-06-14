using CodeSmells;
using CodeSmells.Interfaces;
using CodeSmells.Services;

namespace MooGame;

class Program
{

    public static void Main(string[] args)
    {
        var highScoreService = new HighScoreService(); // kanske göra ett interface av den??
        IInputOutput ioService = new InputOutputService();
        GameController controller = new GameController(ioService, highScoreService);
        controller.StartGame();
    }
}

