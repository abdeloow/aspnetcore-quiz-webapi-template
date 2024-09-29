using System.ComponentModel.DataAnnotations;

namespace QuizApi;

public class Question
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string QuestionText { get; set; } = null!;
    [Required]
    public string Type { get; set; } = null!;
    [Required]
    public string Difficulty { get; set; } = null!;
    [Required]
    public string Category { get; set; } = null!;
    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();
}

