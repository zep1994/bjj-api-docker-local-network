using BjjTrainer_API.Models.Lessons;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BjjTrainer_API.Models.User
{
    [Table("ApplicationUsers")]
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    }
}