namespace QuizApi;

public class QuizSessionQuestionDto
{
    public int QuestionId { get; set; }
    public string Text { get; set; }
    public List<AnswerSubmissionDto> Answers { get; set; }
    public int SelectedAnswerId { get; set; }
    public bool IsCorrect { get; set; }
}
