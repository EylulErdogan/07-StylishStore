using System.ComponentModel.DataAnnotations;

namespace ShoeStoreMvc.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Ad soyad zorunludur")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı zorunludur")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email zorunludur")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola zorunludur")]
        [DataType(DataType.Password)]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "Minimum 8 karakter olmalıdır")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Parola tekrarı zorunludur")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Parolalar eşleşmedi")]
        public string ConfirmPassword { get; set; }
    }
}
