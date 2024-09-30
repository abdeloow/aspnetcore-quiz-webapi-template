using Microsoft.AspNetCore.Identity;

namespace QuizApi;

public interface IAuthService
{
    Task<string> GenerateJwtTokenAsync(ApplicationUser user);
    Task<IdentityResult> RegisterUserAsync(RegisterModelDto model);
    Task<string> LoginUserAsync(LoginModelDto loginUserDto);
    Task LogoutUserAsync();
}
