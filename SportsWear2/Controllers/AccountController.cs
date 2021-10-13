using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsWear2.Data;
using SportsWear2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWear2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private ApplicationDbContext context;

        public AccountController(UserManager<IdentityUser> userManager, ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
              SignInManager<IdentityUser> signInManager)
        {
            this.context = context;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Login()
        {
            CheckSystemAdminCredential();
            return View();
        }

        private async void CheckSystemAdminCredential()
        {
            const string UserName = "Admin";
            const string email = "@gmail.com";
            const string role = "Admin";

            var userDetails = userManager.FindByEmailAsync(email);

            if (userDetails.Result == null)
            {

                IdentityRole roles = new IdentityRole();
                roles.Name = role;
                IdentityResult roleResult = roleManager.
                CreateAsync(roles).Result;

                var user = new IdentityUser
                {
                    UserName = UserName,
                    Email = email,
                    EmailConfirmed = true,
                };
                IdentityResult result = userManager.CreateAsync(user, "Abcd1234!").Result;



                if (result.Succeeded)
                {

                    userManager.AddToRoleAsync(user,
                                         role).Wait();
                }
                Task<bool> hasRole = roleManager.RoleExistsAsync("Client");
                // hasRole.Wait();
                if (!hasRole.Result)
                {
                    IdentityRole role1 = new IdentityRole();
                    role1.Name = "Client";
                    IdentityResult roleResult1 = roleManager.
                    CreateAsync(role1).Result;

                }
                

            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string ReturnUrl)
        {

            if (ModelState.IsValid)
            {
                var user = signInManager.UserManager.Users.Where(u => u.Email == model.Email).FirstOrDefault();
                var result = await signInManager.PasswordSignInAsync(
                    user.UserName, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    context.Users.Update(user);
                    await context.SaveChangesAsync();
                    if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Copy data from RegisterViewModel to IdentityUser
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = true,
                };

                // Store user data in AspNetUsers database table
                var result = await userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Invalid User Register Attempt! Duplicate Email Address");
                    return View(model);
                }

                var role = await roleManager.FindByNameAsync("Client");
                if (role == null)
                {
                    ViewBag.ErrorMessage = $"Role with Id = {role} cannot be found";
                    return View("error");
                }

                ///Add Role
                var id = await userManager.FindByNameAsync(user.UserName);
                var roleResult = await userManager.AddToRoleAsync(id, role.Name);



                if (result.Succeeded)
                {
                    ViewBag.Message = "User Register Successfully";
                    return View();
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult AdminUser()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Copy data from RegisterViewModel to IdentityUser
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = true,
                };

                // Store user data in AspNetUsers database table
                var result = await userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Invalid User Register Attempt! Duplicate Email Address");
                    return View(model);
                }

                var role = await roleManager.FindByNameAsync("Admin");
                if (role == null)
                {
                    ViewBag.ErrorMessage = $"Role with Id = {role} cannot be found";
                    return View("error");
                }

                ///Add Role
                var id = await userManager.FindByNameAsync(user.UserName);
                var roleResult = await userManager.AddToRoleAsync(id, role.Name);



                if (result.Succeeded)
                {
                    ViewBag.Message = "User Register Successfully";
                    return View();
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
