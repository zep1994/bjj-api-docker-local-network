using BjjTrainer_API.Models.Lessons;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BjjTrainer_API.Models.User
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Lesson>? Lessons { get; set; }
    }
}
