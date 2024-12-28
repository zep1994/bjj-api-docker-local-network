using BjjTrainer_API.Models.Users;
using BjjTrainer_API.Services_API.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace BjjTrainer_API.Controllers.Users
{
    [Route("api/auth/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtTokenService _jwtTokenService;
        private readonly ILogger<AuthController> _logger;


        public AuthController(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              JwtTokenService jwtTokenService,
                              ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
            _logger = logger;

        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] RegisterModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                SchoolId = model.SchoolId,  // Associate user with a school
                IsCoach = model.IsCoach     // Mark if the user is a coach
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errorMessages = result.Errors.Select(e => e.Description).ToList();
                foreach (var error in errorMessages)
                {
                    _logger.LogWarning("Signup failed for user: {Username}. Error: {Error}", model.Username, error);
                }
                return BadRequest(new { Errors = errorMessages });
            }

            try
            {
                var claims = new List<Claim>
                {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name, user.UserName),
                new("IsCoach", user.IsCoach.ToString()),  // Pass the IsCoach claim
                new("SchoolId", user.SchoolId?.ToString() ?? string.Empty)  // Include SchoolId if available
                };

                var token = _jwtTokenService.GenerateToken(claims);
                _logger.LogInformation("User signed up successfully: {Username}", model.Username);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating token for user: {Username}", model.Username);
                return StatusCode(500, "An error occurred during token generation.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return Unauthorized("Invalid login attempt");
            }

            // Generate JWT token with the IsCoach claim
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new("IsCoach", user.IsCoach.ToString()) // Add the IsCoach claim
            };

            var token = _jwtTokenService.GenerateToken(claims); // Updated to pass claims

            return Ok(new { Token = token });
        }


        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshToken refreshTokenDto)
        {
            var newTokens = await _jwtTokenService.RefreshTokenAsync(refreshTokenDto.Token);
            if (newTokens == null)
                return Unauthorized("Invalid or expired refresh token.");

            return Ok(newTokens);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Here, implement any logic for token invalidation if necessary
            return Ok(new { Message = "Logged out successfully" });
        }
    }

    public class RegisterModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? SchoolId { get; set; } 
        public bool IsCoach { get; set; }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
