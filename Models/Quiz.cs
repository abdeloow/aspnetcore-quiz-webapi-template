using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApi;

public class Quiz
{
    [Key]
    public int Id { get; set; }
    public int Score { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    [NotMapped]
    public string UserId { get; set; } = null!;
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
