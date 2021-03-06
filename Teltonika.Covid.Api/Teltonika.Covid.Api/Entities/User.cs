using System.ComponentModel.DataAnnotations;

namespace Teltonika.Covid.Api.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(64)]
        public string Password { get; set; }
    }
}
