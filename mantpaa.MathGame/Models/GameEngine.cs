using System.Diagnostics;

namespace MathGame.Models;

internal class GameEngine
{
    List<GameData> gameHistory = new List<GameData>();
    int difficulty = 1;

    public void PlayGame()
    {
        GameData gameData;
        Question[] questions = InitializeGame(difficulty);
        int score = 0;


        Console.WriteLine("Starting the game! Answer the following questions:");
        Console.WriteLine("To give up, type 'exit'.");
        for (int i = 0; i < questions.Length; i++)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine($"Question {i + 1}: {questions[i].Equation}");
            string? userAnswer = Console.ReadLine();
            stopwatch.Stop();

            if (userAnswer != null && userAnswer.ToLower() == "exit")
            {
                Console.WriteLine("Exiting the game. Goodbye!");
                break;
            }

            if (userAnswer != null)
            {
                questions[i].Answer = userAnswer;
                questions[i].TimeSpentSeconds = stopwatch.Elapsed.Seconds;
                if (userAnswer == questions[i].ExpectedAnswer)
                {
                    Console.WriteLine("Correct!");
                    score++;
                }
                else
                {
                    Console.WriteLine($"Wrong! The correct answer is {questions[i].ExpectedAnswer}");
                }
            }
        }
        gameData = new GameData(score, difficulty, questions);

        gameHistory.Add(gameData);
    }

    public void ViewGameHistory()
    {
        Console.WriteLine("Score history:");
        for (int i = 0; i < gameHistory.Count; i++)
        {
            Console.WriteLine($"Game {i + 1}:\n{gameHistory[i].ToString()}\n");
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    public void ChangeDifficulty()
    {
        string? input;
        Console.WriteLine("Current difficulty level: " + difficulty);
        Console.WriteLine("Enter new difficulty level (1-3):");
        input = Console.ReadLine();

        while (input == null || !new string[] { "1", "2", "3" }.Contains(input))
        {
            
            Console.WriteLine("Invalid input, try again.");
            input = Console.ReadLine();
        }

        difficulty = int.Parse(input);
    }

    private Question[] InitializeGame(int difficultyLevel)
    {
        Random random = new Random();
        int questionsToAsk = 5;
        Question[] questions = new Question[questionsToAsk];
        for (int i = 0; i < questionsToAsk; i++)
        {
            string op = GetOperator(random);
            questions[i] = CreateQuestion(op, difficultyLevel, random);
        }

        return questions;
    }

    private Question CreateQuestion(string op, int difficultyLevel, Random random)
    {
        if (op == "/")
        {
            int dividend = 1;
            int divisor = 1;
            bool valid = false;
            while (valid == false)
            {
                dividend = random.Next(0, 101);
                divisor = random.Next(1, dividend);
                if (dividend % divisor == 0)
                {
                    valid = true;
                }
            }

            string equation = $"{dividend} / {divisor}";
            string expectedAnswer = (dividend / divisor).ToString();
            return new Question(equation, "", expectedAnswer);
        }

        else if (new string[] { "+", "-", "*" }.Contains(op)) // Perhaps make this list of operators static and outside of the method?
        {
            int num1 = random.Next(1, 10 * difficultyLevel);
            int num2 = random.Next(1, 10 * difficultyLevel);
            string equation = $"{num1} {op} {num2}";
            string expectedAnswer = "";
            if (op == "+")
                expectedAnswer = (num1 + num2).ToString();
            else if (op == "-")
                expectedAnswer = (num1 - num2).ToString();
            else if (op == "*")
                expectedAnswer = (num1 * num2).ToString();
            else
                expectedAnswer = "issue occured";

            return new Question(equation, "", expectedAnswer);
        }

        else
        {
            throw new InvalidDataException("Invalid operator: " + op);
        }
    }

    // Pick a random operator for the question, to help randomize the questions a bit more.
    private string GetOperator(Random random)
    {
        int val = random.Next(1, 5); // 5 is a bit magical, perhaps make this a static list of operators and pick a random index from it instead?
        switch (val)
        {
            case 1:
                return "+";
            case 2:
                return "-";
            case 3:
                return "*";
            case 4:
                return "/";
            default:
                return "+";
        }
    }
}