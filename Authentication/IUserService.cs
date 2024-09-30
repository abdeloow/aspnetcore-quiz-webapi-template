namespace QuizApi;

public interface IUserService
{
    string GetCurrentUserId();
    Task<ApplicationUser> GetCurrentUserAsync();
}
