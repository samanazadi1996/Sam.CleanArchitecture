using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Data;
using TestTemplate.Application.DTOs.Account.Requests;
using TestTemplate.Application.Helpers;
using TestTemplate.Application.Interfaces;
using TestTemplate.Infrastructure.Identity.Models;

namespace TestTemplate.WebUi.Controllers
{
    public class AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITranslator translator) : Controller
    {
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(AuthenticationRequest model)
        {
            if (ModelState.IsValid)
            {
                var username = model.UserName.Trim();
                var user = await userManager.FindByNameAsync(username);

                if (user is null)
                {
                        ModelState.AddModelError(nameof(model.UserName), translator.GetString(TranslatorMessages.AccountMessages.Account_notfound_with_UserName(model.UserName)));
                        return View(model);
                }

                var checkPasswordSignInAsync = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (!checkPasswordSignInAsync.Succeeded)
                {
                    ModelState.AddModelError(nameof(model.Password), translator.GetString(TranslatorMessages.AccountMessages.Invalid_password()));
                    return View(model);
                }

                var result = await signInManager.PasswordSignInAsync(user.UserName, model.Password, true, false);

                if (result.Succeeded)
                    return Redirect("/");
            }

            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return Redirect("/");
        }


    }
}
