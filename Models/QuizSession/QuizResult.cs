namespace QuizApi;

public class QuizResult
{
    public string UserId { get; set; } = null!;
    public int Score { get; set; }
    public int TotalQuestions { get; set; }
    public DateTime CompletionTime { get; set; }
    public List<AnswerDto> UsersAnswers { get; set; } = new List<AnswerDto>();
}
