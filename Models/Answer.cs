using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApi;

public class Answer
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Text { get; set; } = null!;
    [Required]
    public bool IsCorrect { get; set; }
    [ForeignKey("Question")]
    public int QuestionId { get; set; }
    public virtual Question Question { get; set; }
}
