namespace MathGame.Models;

internal class Question
{
    public string Equation { get; set; }
    public string Answer { get; set; }

    public string ExpectedAnswer { get; set; }

    public int TimeSpentSeconds { get; set; } = -1; // Todo: better value?
    public Question(string equation, string answer, string expectedAnswer)
    {
        Equation = equation;
        Answer = answer;
        ExpectedAnswer = expectedAnswer;
    }

}