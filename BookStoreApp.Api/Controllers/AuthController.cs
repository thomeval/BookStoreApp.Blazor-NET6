using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreApp.Api.Data;
using BookStoreApp.Api.Models.User;
using BookStoreApp.Api.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookStoreApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<ApiUser> _userManager;
    private readonly IConfiguration _config;

    public AuthController(ILogger<AuthController> logger, IMapper mapper, UserManager<ApiUser> userManager, IConfiguration config)
    {
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
        _config = config;
    }

    [HttpPost]
    [Route("register")]
    [ProducesResponseType(202)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Register(UserDto userDto)
    {
        _logger.LogInformation($"Registration Attempt for {userDto.Email}");
        try
        {
            var user = _mapper.Map<ApiUser>(userDto);
            user.UserName = userDto.Email;
            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }


            await _userManager.AddToRoleAsync(user, "User");
            return Accepted();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error Performing GET in {nameof(Register)}");
            return this.ServerError();
        }
    }

    [HttpPost]
    [Route("login")]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<AuthResponse>> Login(UserLoginDto userLoginDto)
    {
        _logger.LogInformation($"Login Attempt for {userLoginDto.Email}");
        try
        {
            var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
            var passwordValid = await _userManager.CheckPasswordAsync(user, userLoginDto.Password);
            if (user == null || !passwordValid)
            {
                return Unauthorized(userLoginDto);
            }

            var tokenString = await GenerateToken(user);

            var response = new AuthResponse { Email = user.Email, Token = tokenString, UserId = user.Id };
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error Performing GET in {nameof(Register)}");
            return this.ServerError();
        }
    }

    private async Task<string> GenerateToken(ApiUser user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(e => new Claim(ClaimTypes.Role, e)).ToList();
        var userClaims = await _userManager.GetClaimsAsync(user);

        var claims = new List<Claim>
        {
            // Subject
            new (JwtRegisteredClaimNames.Sub, user.UserName ),
            // Unique JWT token identifier
            new (JwtRegisteredClaimNames.Jti,  Guid.NewGuid().ToString()),
            new (JwtRegisteredClaimNames.Email,  user.Email),
            new (CustomClaimTypes.UID, user.Id),

        };

        claims.AddRange(roleClaims);
        claims.AddRange(userClaims);

        var token = new JwtSecurityToken
        (

            issuer: _config["JwtSettings:Issuer"],
            audience: _config["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_config["JwtSettings:Duration"])),
            signingCredentials: credentials

        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}


