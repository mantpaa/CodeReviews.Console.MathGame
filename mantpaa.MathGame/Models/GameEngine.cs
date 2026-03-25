using System.Diagnostics;

namespace MathGame.Models;

internal class GameEngine
{
    string[] operators = new string[] { "+", "-", "*", "/" };
    List<GameData> gameHistory = new List<GameData>();
    int difficulty = 1;
    GameType gameType = GameType.Addition;

    public void PlayGame()
    {
        GameData gameData;
        
        int score = 0;


        Console.WriteLine("Starting the game! Answer the following questions:");
        Console.WriteLine("To give up, type 'exit'.");
        
        Question[] questions = InitializeGame(difficulty);
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
    public void ChangeQuestionType()
    {
        string? input;
        Console.WriteLine("Enter question type:\na - addition\ns - subtraction\nm - multiplication\nd - division\nr - random!");
        input = Console.ReadLine();

        while (input == null || !new string[] { "a", "s", "m", "d", "r" }.Contains(input.ToLower()))
        {
            Console.WriteLine("Invalid input, try again.");
            input = Console.ReadLine();
        }
       
        switch (input.ToLower()) 
        {
            case "a":
                gameType = GameType.Addition;
                break;
            case "s":
                gameType = GameType.Subtraction;
                break;
            case "m":
                gameType = GameType.Multiplication;
                break;
            case "d":
                gameType = GameType.Division;
                break;
            case "r":
                gameType = GameType.Random;
                break;
        }
    }
    private Question[] InitializeGame(int difficultyLevel)
    {
        Random random = new Random();
        string[] operatorValue = new string[] { "+", "-", "*", "/" }; // 0:+, 1: -, 2: *, 3: / : GameType enum values correspond to these indices.
        int questionsToAsk = 5;
        Question[] questions = new Question[questionsToAsk];
        for (int i = 0; i < questionsToAsk; i++)
        {
            string op = "+";

            if (gameType == GameType.Random)
            {
                op = GetOperator(random);
            }
            else
            {
                op = operatorValue[(int) gameType];
            }

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
                int minValue = (difficultyLevel > 1) ? 2 : 1; // disallow division by 1 for higher difficulties.
                divisor =  random.Next(minValue, (dividend > minValue) ? dividend : minValue +1); // Ensure maxval is larger than minval
                if (dividend % divisor == 0)
                {
                    valid = true;
                }
            }

            string equation = $"{dividend} / {divisor}";
            string expectedAnswer = (dividend / divisor).ToString();
            return new Question(equation, "", expectedAnswer);
        }

        else if (new string[] {"+", "*", "-" }.Contains(op))
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
        int val = random.Next(0, operators.Length);
        return operators[val];
    }

    enum GameType
    {
        Addition,
        Subtraction,
        Multiplication,
        Division,
        Random
    }
}