namespace MathGame.Models;

internal class GameData
{
    private int score;
    private double avgTimePerQuestion;
    private int difficultyLevel;
    private Question[] questions;

    public GameData(int score, int difficultyLevel, Question[] questions)
    {
        this.score = score;

        this.difficultyLevel = difficultyLevel;
        this.questions = questions;
        this.avgTimePerQuestion = CalculateAvgTimePerQuestion();
    }

    public override string ToString()
    {
        string returnString = "";
        foreach (var question in questions)
        {
            returnString += $"Equation: {question.Equation} = ?, Answer: {question.Answer}, Expected answer: {question.ExpectedAnswer}, Time spent: {((question.TimeSpentSeconds) >= 0 ? (question.TimeSpentSeconds) : "N/A")} seconds.\n";
        }
        returnString += "".PadLeft(Console.BufferWidth, '-') + "\n";
        returnString += $"Score: {score}, Difficulty level: {difficultyLevel}, Average time per question: {avgTimePerQuestion} seconds.";
        return returnString;
    }

    private double CalculateAvgTimePerQuestion()
    {
        int totalTime = 0;
        int questionsAnswered = 0;
        foreach (var question in questions)
        {
            if (question.TimeSpentSeconds >= 0)
            {
                totalTime += question.TimeSpentSeconds;
                questionsAnswered++;
            }
        }

        return questionsAnswered > 0 ? (double)totalTime / questionsAnswered : 0;
    }
}