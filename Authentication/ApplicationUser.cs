using Microsoft.AspNetCore.Identity;

namespace QuizApi;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Guid? CurrentQuizSessionId { get; set; }
    public virtual QuizSession? QuizSession { get; set; }
}
