using BjjTrainer_API.Models.DTO.UserDtos;
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

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateSchool([FromBody] SchoolCreateRequest request)
        {
            // Get the currently logged-in user (the coach)
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not logged in.");
            }

            var result = await _schoolService.CreateSchoolAndAddCoachAsync(userId, request.Name, request.Address, request.Phone);

            if (result.Contains("already a member"))
            {
                return Conflict(result); // Send conflict if user is already a member of another school
            }

            return Ok(result);
        }
    }
}
