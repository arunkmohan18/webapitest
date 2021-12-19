using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWebAPI.DTOs
{
    public class UserRegisterDTO
    {
        [Required]
        public string UserName{get; set;}
        [Required]
        [StringLength(8,MinimumLength=4,ErrorMessage="Password length must be 4 to 8 characters")]
        public string Password{get; set;}
    }
}