using BjjTrainer_API.Models.Lessons;
using Microsoft.AspNetCore.Identity;

namespace BjjTrainer_API.Models.User
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    }
}
