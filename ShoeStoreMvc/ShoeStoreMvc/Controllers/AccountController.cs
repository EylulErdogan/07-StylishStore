using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SeninMvcProjeAdi.Models;
using ShoeStoreMvc.ViewModels;

namespace ShoeStoreMvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(SignInManager<Users> signInManager, UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;

        }

        public IActionResult Login()
        {
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> CreateAdmin()
        {
            string adminEmail = "admin@gmail.com";
            string adminPassword = "Admin12345";

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                Users user = new Users
                {
                    FullName = "Admin Kullanıcı",
                    Email = adminEmail,
                    UserName = adminEmail
                };

                var result = await userManager.CreateAsync(user, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    model.RememberMe,
                    false
                );

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Email veya parola hatalı");
            }

            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Users user = new Users
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    UserName = model.Email
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        public IActionResult VerifyEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "Bu email adresine ait kullanıcı bulunamadı");
                    return View(model);
                }

                TempData["OpenChangePasswordModal"] = "true";
                TempData["ResetEmail"] = user.UserName;

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public IActionResult ChangePassword(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("VerifyEmail", "Account");
            }

            return View(new ChangePasswordViewModel
            {
                Email = username
            });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (string.IsNullOrEmpty(model.Email) ||
                string.IsNullOrEmpty(model.NewPassword) ||
                string.IsNullOrEmpty(model.ConfirmPassword))
            {
                TempData["OpenChangePasswordModal"] = "true";
                TempData["ResetEmail"] = model.Email;
                TempData["PasswordError"] = "Tüm alanları doldurunuz.";
                return RedirectToAction("Index", "Home");
            }

            if (model.NewPassword != model.ConfirmPassword)
            {
                TempData["OpenChangePasswordModal"] = "true";
                TempData["ResetEmail"] = model.Email;
                TempData["PasswordError"] = "Şifreler eşleşmiyor.";
                return RedirectToAction("Index", "Home");
            }

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                TempData["OpenChangePasswordModal"] = "true";
                TempData["ResetEmail"] = model.Email;
                TempData["PasswordError"] = "Kullanıcı bulunamadı.";
                return RedirectToAction("Index", "Home");
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var result = await userManager.ResetPasswordAsync(user, token, model.NewPassword);

            if (result.Succeeded)
            {
                TempData["OpenLoginModal"] = "true";
                TempData["PasswordSuccess"] = "Şifreniz başarıyla güncellendi.";
                return RedirectToAction("Index", "Home");
            }

            TempData["OpenChangePasswordModal"] = "true";
            TempData["ResetEmail"] = model.Email;
            TempData["PasswordError"] = string.Join(" ", result.Errors.Select(x => x.Description));

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}