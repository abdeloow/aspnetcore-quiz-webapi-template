using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace QuizApi;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<ApplicationUser> GetCurrentUserAsync()
    {
        var userId = GetCurrentUserId();
        if (userId == null)
        {
            return null;
        }
        return await _userManager.FindByIdAsync(userId);
    }

    public string GetCurrentUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        return userId;
    }
}
