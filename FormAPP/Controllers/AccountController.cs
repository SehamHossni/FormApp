using FormAPP.Models;
using FormAPP.ViewModel;
using LoginFormAttempts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace FormAPP.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
      
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager ,ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this._context = context;    
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel UserVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = new ApplicationUser();
                userModel.UserName = UserVM.UserName;
                userModel.PasswordHash = UserVM.Password;
                userModel.Address = UserVM.Address;
                IdentityResult result = await userManager.CreateAsync(userModel, UserVM.Password);
                if (result.Succeeded)
                {

                    await signInManager.SignInAsync(userModel, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }

      

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.UserName);

                if (user != null)
                {
                        var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: true);
                        
                        if (result.Succeeded)
                        {
                         
                            await userManager.UpdateAsync(user);
                            return RedirectToAction("Index" , "Home");
                        }

                        if (result.IsLockedOut)
                        {
                            ModelState.AddModelError(string.Empty, "Your account is locked. Try again after 30 seconds.");
                            
                        }
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        await userManager.UpdateAsync(user);
                }
            }

            return View(model);
        }


        public async Task<IActionResult> GetLoggedInUsers()
        {
            var loggedInUsers = signInManager.UserManager.Users.Where(u => signInManager.IsSignedIn(User));
            var orderedUsers = loggedInUsers.OrderBy(u => u.UserName).ToList();

            return View(orderedUsers);
        }



        public async Task<IActionResult> LoggOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}
