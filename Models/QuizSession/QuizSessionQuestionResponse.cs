using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApi;

public class QuizSessionQuestionResponse
{
    [Key]
    public Guid Id { get; set; }
    [ForeignKey("QuizSession")]
    public Guid QuizSessionId { get; set; }
    public QuizSession QuizSession { get; set; } = null!;
    [ForeignKey("Question")]
    public int QuestionId { get; set; }
    public virtual Question Question { get; set; } = null!;
    public int? SelectedAnswerId { get; set; }
    public virtual Answer SelectedAnswer { get; set; } = null!;
    public bool IsCorrect { get; set; }
}
