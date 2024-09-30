namespace QuizApi;

public class QuizSessionDto
{
    public Guid SessionId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public bool IsCompleted { get; set; }
    public List<QuizSessionQuestionDto> Questions { get; set; } = new List<QuizSessionQuestionDto>();
}
