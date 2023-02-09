using System.ComponentModel.DataAnnotations;

namespace SampleWebApi._4.ViewModels.Users
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "وارد کردن نام کاربری الزامی است")]
        public string? Username { get; set; }


        [Required(ErrorMessage = "رمز عبور را به درستی وارد نمایید")]
        public string? Password { get; set; }
    }
}
