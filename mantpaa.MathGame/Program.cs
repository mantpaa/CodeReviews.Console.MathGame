using MathGame.Models;

namespace MathGame;

public static class MathGame
{
    public static void Main()
    {
        GameEngine gameEngine = new GameEngine();

        string input = "";
        do
        {
            Console.WriteLine("Math game!\n" +
                "Press the number to choose a menu option.\n"+
                "1. Play game\n"+
                "2. See history.\n"+
                "3. Change difficulty(1-3).\n"+
                "4. Change question type\n"
                +"Exit to exit.");
            string? userInput = Console.ReadLine();

            if (userInput != null)
            {
                input = userInput.ToLower();
                switch (input)
                {
                    case "1":
                         gameEngine.PlayGame();
                        break;
                    case "2":
                        gameEngine.ViewGameHistory();
                        break;
                    case "3":
                        gameEngine.ChangeDifficulty();
                        break;
                    case "4":
                        gameEngine.ChangeQuestionType();
                        break;
                    case "exit":
                        Console.WriteLine("Exiting the game. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid input, try again.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        } while (input != "exit");
    }
}
