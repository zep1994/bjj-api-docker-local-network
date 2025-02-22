using BjjTrainer_API.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace BjjTrainer_API.Models.Schools
{
    public class School
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Address { get; set; } = string.Empty;

        [MaxLength(15)]
        public string Phone { get; set; } = string.Empty;

        public ICollection<ApplicationUser> Users { get; set; } = [];
    }
}
