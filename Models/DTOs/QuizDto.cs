namespace QuizApi;

public class QuizDto
{
    public ICollection<QuestionDto> QuestionDtos { get; set; }
    public int Score { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
