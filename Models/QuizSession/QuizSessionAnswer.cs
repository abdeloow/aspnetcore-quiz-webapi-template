using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApi;

public class QuizSessionAnswer
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("QuizSessionQuestion")]
    public int QuizSessionQuestionId { get; set; }
    public virtual QuizSessionQuestion QuizSessionQuestion { get; set; } = null!;
    [ForeignKey("Answer")]
    public int SelectedAnswerId { get; set; }
    public virtual Answer SelectedAnswer { get; set; } = null!;
}
