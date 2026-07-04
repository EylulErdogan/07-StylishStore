using System.ComponentModel.DataAnnotations;

namespace ShoeStoreMvc.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Email zorunludur")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola zorunludur")]
        [DataType(DataType.Password)]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "Minimum 8 karakter")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Parola tekrarı zorunludur")]
        [DataType(DataType.Password)]
        [Display(Name = "Yeni Parola Tekrar")]
        [Compare("NewPassword", ErrorMessage = "Parolalar eşleşmiyor")]
        public string ConfirmNewPassword { get; set; }
    }
}
