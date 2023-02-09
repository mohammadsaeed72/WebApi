using System.ComponentModel.DataAnnotations;

namespace SampleWebApi._4.ViewModels.Users
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "نام کاربری را وارد نمایید")]
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "ایمیل را وارد نمایید")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "رمز عبور را وارد نمایید")]
        public string? Password { get; set; }
    }
}
