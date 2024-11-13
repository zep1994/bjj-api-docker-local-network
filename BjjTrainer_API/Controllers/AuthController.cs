using BjjTrainer_API.Models.User;
using BjjTrainer_API.Services_API;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace BjjTrainer_API.Controllers
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
            var user = new ApplicationUser { UserName = model.Username, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errorMessages = result.Errors.Select(e => e.Description).ToList();
                // Log each error with more context
                foreach (var error in errorMessages)
                {
                    // Use warning level for signup failures and include user information
                    _logger.LogWarning("Signup failed for user: {Username}. Error: {Error}", model.Username, error);
                }
                return BadRequest(new { Errors = errorMessages });
            }

            try
            {
                var token = _jwtTokenService.GenerateToken(user.Id, user.UserName);
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
                _logger.LogWarning("Invalid login attempt for user: {Username}", model.Username);
                return Unauthorized("Invalid login attempt");
            }

            var token = _jwtTokenService.GenerateToken(user.Id, user.UserName);
            _logger.LogInformation("User logged in successfully: {Username}", model.Username);
            return Ok(new { Token = token });
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
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
