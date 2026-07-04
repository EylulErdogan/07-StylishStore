using Microsoft.AspNetCore.Identity;

namespace SeninMvcProjeAdi.Models
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; }
    }
}