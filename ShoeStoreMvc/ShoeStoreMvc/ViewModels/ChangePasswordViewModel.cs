using System.ComponentModel.DataAnnotations;

namespace ShoeStoreMvc.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string Email { get; set; }

        [Required(ErrorMessage = "Yeni şifre boş geçilemez")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Şifre tekrar boş geçilemez")]
        [Compare("NewPassword", ErrorMessage = "Şifreler eşleşmiyor")]
        public string ConfirmPassword { get; set; }
    }
}