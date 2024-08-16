using CodeSmells;
using CodeSmells.GameEngines;
using CodeSmells.Interfaces;
using CodeSmells.Services;

namespace MooGame;

class Program
{

    public static void Main(string[] args)
    {
        IInputOutput ioService = new InputOutputService();

        //Våran meny för att välja spel, vi anger även filnamn för att hämta och skriva till rätt highscore-lista
        (IGameEngine gameEngine, IHighScore highScoreService) = ChooseGame(ioService);

        GameController controller = new GameController(ioService, highScoreService, gameEngine);
        controller.StartGame();
    }

    private static (IGameEngine, IHighScore) ChooseGame(IInputOutput ioService) // kanske inte snyggaste lösningen, men snyggare! potentiellt se över ett factory pattern?
    {
        ioService.WriteLine("Choose a game:" + Environment.NewLine + "1. Bulls and Cows" + Environment.NewLine + "2. Wins and Losses" + Environment.NewLine + "3. Mastermind");
        string gameChoice = ioService.ReadLine();

        switch (gameChoice)
        {
            case "1":
                ioService.WriteLine("Selected game: Bulls and Cows" + Environment.NewLine);
                return (new BullsAndCowsGameEngine(), new HighScoreService("BullsAndCowScores.txt"));
            case "2":
                ioService.WriteLine("Selected game: Ws and Ls" + Environment.NewLine);
                return (new WsAndLsGameEngine(), new HighScoreService("WsAndLsScores.txt"));
            case "3":
                ioService.WriteLine("Selected game: Mastermind" + Environment.NewLine);
                return (new MastermindGameEngine(), new HighScoreService("MastermindScores.txt"));
            default:
                ioService.WriteLine("Invalid choice, defaulting to Bulls and Cows" + Environment.NewLine);
                return (new BullsAndCowsGameEngine(), new HighScoreService("BullsAndCowScores.txt"));
        }
    } 
}

/*
 //IHighScore highScoreService = new HighScoreService();
        IInputOutput ioService = new InputOutputService();
        //IGameEngine gameEngine = new BullsAndCowsGameEngine();
        IGameEngine gameEngine;
        IHighScore highScoreService;

        //Menyn för att välja spel // Verkligen en placeholder för att testa min idé, ska försöka att lösa dett i Controllern
        ioService.WriteLine("Choose a game:" + Environment.NewLine + "1. Bulls and Cows" + Environment.NewLine + "2. Wins and Losses");
        string gameChoice = ioService.ReadLine();
        
        switch (gameChoice)
        {
            case "1":
                gameEngine = new BullsAndCowsGameEngine();
                highScoreService = new HighScoreService("BullsAndCowScores.txt");
                ioService.WriteLine("Selected game: Bulls and Cows");
                break;
            case "2":
                gameEngine = new WsAndLsGameEngine();
                highScoreService = new HighScoreService("WsAndLsScores.txt");
                ioService.WriteLine("Selected game: Ws and Ls");
                break;
            default:
                //ioService.WriteLine("Invalid choice, starting Bulls and Cows");
                gameEngine = new BullsAndCowsGameEngine();
                highScoreService = new HighScoreService("BullsAndCowScores.txt");
                ioService.WriteLine("Invalid choice, starting Bulls and Cows");
                break;
        }
        //

        GameController controller = new GameController(ioService, highScoreService, gameEngine);
        controller.StartGame();
 */

