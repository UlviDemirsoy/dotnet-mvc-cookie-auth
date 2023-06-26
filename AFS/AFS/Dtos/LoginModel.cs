using System.ComponentModel.DataAnnotations;

namespace AFS.Dtos
{
    public class LoginModel
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
       
    }
}
