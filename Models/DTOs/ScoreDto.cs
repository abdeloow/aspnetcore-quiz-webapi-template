namespace QuizApi;

public class ScoreDto
{
    public Guid SessionId { get; set; }
    public int TotalQuestions { get; set; }
    public int CorrectAnswers { get; set; }
    public int Score { get; set; }
}
