using System.ComponentModel.DataAnnotations;

namespace QuizApi;

public class RegisterModelDto
{
    [Required]
    public string UserName { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
}
