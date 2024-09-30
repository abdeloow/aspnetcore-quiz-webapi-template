
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace QuizApi;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtSettings _jwtSettings;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<IdentityResult> RegisterUserAsync(RegisterModelDto registerUserDto)
    {
        // Check if user already exists
        var existingUser = await _userManager.FindByNameAsync(registerUserDto.UserName);
        if (existingUser != null)
        {
            throw new Exception("User with this username already exists.");
        }

        var existingEmail = await _userManager.FindByEmailAsync(registerUserDto.Email);
        if (existingEmail != null)
        {
            throw new Exception("User with this email already exists.");
        }

        // Create a new user
        var user = new ApplicationUser
        {
            UserName = registerUserDto.UserName,
            Email = registerUserDto.Email
        };

        // Save user to the database
        var result = await _userManager.CreateAsync(user, registerUserDto.Password);
        return result;
    }

    public async Task<string> LoginUserAsync(LoginModelDto loginUserDto)
    {
        var result = await _signInManager.PasswordSignInAsync(loginUserDto.UserName, loginUserDto.Password, isPersistent: false, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            throw new Exception("Invalid login attempt.");
        }

        // Get user by username
        var user = await _userManager.FindByNameAsync(loginUserDto.UserName);
        return await GenerateJwtTokenAsync(user);
    }

    public async Task LogoutUserAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        authClaims.AddRange(userClaims);
        authClaims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: DateTime.Now.AddMinutes(_jwtSettings.ExpirationInMinutes),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
