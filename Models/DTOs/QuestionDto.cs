namespace QuizApi;

public class QuestionDto
{
    public string QuestionText { get; set; }
    public string Type { get; set; }
    public string Difficulty { get; set; }
    public string Category { get; set; }
    public ICollection<AnswerDto> AnswerDtos { get; set; }
}
