using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sim6.Models;
using Sim6.ViewModel.Account;
using System.Threading.Tasks;

namespace Sim6.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _singManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> singManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _singManager = singManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            AppUser user = new()
            {
                Email = vm.Email,
                FullName = vm.FullName,
                UserName = vm.UserName
            };

            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(vm);
            }
            await _userManager.AddToRoleAsync(user, "User");
            await _singManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            var user = await _userManager.FindByEmailAsync(vm.Email);
            if(user is null)
            {
                ModelState.AddModelError("", "Email or password is incorrect");
                return View(vm);
            }

            var result = await _singManager.PasswordSignInAsync(user, vm.Password, false, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email or password is incorrect");
                return View(vm);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _singManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        //public async Task<IActionResult> CreateRoles()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole()
        //    {
        //        Name="Admin"
        //    });
        //    await _roleManager.CreateAsync(new IdentityRole()
        //    {
        //        Name = "User"
        //    });
        //    return Ok("Create roles");
        //}
    }
}
