using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApi;

public class QuizSessionQuestion
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("QuizSession")]
    public Guid QuizSessionId { get; set; }
    public virtual QuizSession QuizSession { get; set; } = null!;
    [ForeignKey("Question")]
    public int QuestionId { get; set; }
    public virtual Question Question { get; set; } = null!;
    public virtual ICollection<QuizSessionAnswer> QuizSessionAnswers { get; set; } = new List<QuizSessionAnswer>();
}
