using BjjTrainer_API.Models.Users;
using BjjTrainer_API.Services_API.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BjjTrainer_API.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolController : ControllerBase
    {
        private readonly SchoolService _schoolService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SchoolController(SchoolService schoolService, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _schoolService = schoolService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateSchool([FromBody] School school)
        {
            // Get the currently logged-in user
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not logged in.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Unauthorized("User not found.");
            }

            // Check if the user is a coach
            if (!user.IsCoach)
            {
                return Forbid("Only coaches can create schools.");
            }

            if (string.IsNullOrWhiteSpace(school.Name) || string.IsNullOrWhiteSpace(school.Phone))
            {
                return BadRequest("School name and phone are required.");
            }

            var createdSchool = await _schoolService.CreateSchoolAsync(school.Name, school.Address, school.Phone);
            return Ok(createdSchool);
        }
    }
}
