using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VidlyApp.View_Model;

namespace VidlyApp.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<IdentityUser> _UserManager { get; }
        public SignInManager<IdentityUser> _SignInManager { get; }

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel models)
        {
            if (ModelState.IsValid)
            {
                var Users = new IdentityUser { UserName = models.Email, Email = models.Email };
                var results = await _UserManager.CreateAsync(Users, models.Password);
                if (results.Succeeded)
                {
                    await _SignInManager.SignInAsync(Users, isPersistent:false);
                    return RedirectToAction("Login","Account");
                }
            }
            return View(models);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _SignInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "Home");
                }
                ModelState.AddModelError(string.Empty, "invalid password");
            }
            return View(model);
        }
    }
}
