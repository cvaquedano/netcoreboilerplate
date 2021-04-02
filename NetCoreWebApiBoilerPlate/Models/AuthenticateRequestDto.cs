using System.ComponentModel.DataAnnotations;

namespace NetCoreWebApiBoilerPlate.Models
{
    public class AuthenticateRequestDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Email { get; set; }
    }
}
