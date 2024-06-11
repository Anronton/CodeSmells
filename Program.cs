using CodeSmells;
using CodeSmells.Interfaces;
using CodeSmells.Services;

namespace MooGame
{
    class Program
    {

        public static void Main(string[] args)
        {
            IInputOutput ioService = new InputOutputService();
            GameController controller = new GameController(ioService);
            controller.StartGame();
        }
    }
}

