using System.ComponentModel.DataAnnotations;

namespace QuizApi;

public class QuizSession
{
    [Key]
    public Guid SessionId { get; set; }
    [Required]
    public string UserId { get; set; } = null!;
    [Required]
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    [Required]
    public bool IsCompleted { get; set; }
    public virtual ICollection<QuizSessionQuestion> QuizSessionQuestions { get; set; } = new List<QuizSessionQuestion>();
}
