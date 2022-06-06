
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuckyDrawPromotion.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }


    public class UserDTO_AuthenticateRequest
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(.{6,30})$", ErrorMessage = "The Password Confirm is incorrect to the pattern. Password must contain at least 6 characters, including UPPER/lowercase, numbers. Please try again.")]
        public string Password { get; set; } = null!;
    }

    public class UserDTO_AuthenticateResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }

        public UserDTO_AuthenticateResponse(User user, string token)
        {
            Id = user.UserId;
            Email = user.Email;
            Password = user.Password;
            AccessToken = token;
        }
    }

    public class UserDTO_AuthenticatePassword
    {
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string ConfirmPassword { get; set; } = null!;
    }

    public class UserDTO_AuthenticateEmail
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
