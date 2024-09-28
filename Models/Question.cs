using System.ComponentModel.DataAnnotations;

namespace QuizApi;

public class Question
{
    [Key]
    public int Id { get; set; }
    public string QuestionText { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string Difficulty { get; set; } = null!;
    public string Category { get; set; } = null!;
    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();
}

