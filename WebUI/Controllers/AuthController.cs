
using Business.DTOs;
using Business.Services;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using WebUI.ViewModels;
using static WebUI.Utilities.Helper;

namespace WebUI.Controllers;

public class AuthController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMailService _mailService;

    public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager,IMailService mailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _mailService = mailService;

    }


    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(AppUserRegisterVM user)
    {
        if (!ModelState.IsValid) return View(user);

        AppUser appUser = new AppUser()
        {
            Email = user.Email,
            FullName = user.FullName,
            UserName = user.Username,
            IsActive = true
        };
        IdentityResult ıdentityResult = await _userManager.CreateAsync(appUser, user.Password);
        if (!ıdentityResult.Succeeded)
        {

            foreach (var eror in ıdentityResult.Errors)
            {
                ModelState.AddModelError("", eror.Description);
            }
            return View(user);
        }
        await _userManager.AddToRoleAsync(appUser, RoleType.Member.ToString());


        return RedirectToAction(nameof(Login));
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(AppUserLoginVM loginUserVM)
    {
        if (!ModelState.IsValid) return View(loginUserVM);
        var user = await _userManager.FindByEmailAsync(loginUserVM.EmailOrUsername);

        if (user == null)
        {
            user = await _userManager.FindByNameAsync(loginUserVM.EmailOrUsername);
            if (user == null)
            {
                ModelState.AddModelError("", "Password or Username is incorrect");
                return View(loginUserVM);
            }

        }

        var signINResult = await _signInManager.PasswordSignInAsync(user, loginUserVM.Password, loginUserVM.RememberMe, true);

        if (signINResult.IsLockedOut)
        {
            ModelState.AddModelError("", "Try a few moments later");
            return View(loginUserVM);
        }
        if (!signINResult.Succeeded)
        {
            ModelState.AddModelError("", "Password or Username is incorrect");
            return View(loginUserVM);
        }
        if (!user.IsActive)
        {
            ModelState.AddModelError("", "Not found");
        }

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        if (User.Identity.IsAuthenticated)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        return BadRequest();
    }




    public async Task<IActionResult> CreateRoles()
    {
        foreach (var role in Enum.GetValues(typeof(RoleType)))
        {
            if (!await _roleManager.RoleExistsAsync(role.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole(role.ToString()));
            }
        }

        return Json("ok");
    }

    public IActionResult ForgotPassword()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotPasswordVM)
    {
        if (!ModelState.IsValid) return View(forgotPasswordVM);

        var user = await _userManager.FindByEmailAsync(forgotPasswordVM.Email);

        if (user is null)
        {
            ModelState.AddModelError("Email", "Not found");
            return View(forgotPasswordVM);
        }


        string token = await _userManager.GeneratePasswordResetTokenAsync(user);
        string? link = Url.Action("ResetPassword", "Auth", new { userId = user.Id, token = token }, HttpContext.Request.Scheme);

        //await _mailService.SendEmailAsync(new MailRequestDto {ToEmail=user.Email,Subject="Reset Password",Body=$"<a href={link}>click here</a>" });

        return Json(link);

    }

    public async Task<IActionResult> ResetPassword(string userId, string token)
    {

        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
        {
            return BadRequest();
        }
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            return NotFound();
        }

        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordVM, string userId, string token)
    {

        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
        {
            return BadRequest();
        }
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            return NotFound();
        }
        if (!ModelState.IsValid) return View(resetPasswordVM);

        var identityResult = await _userManager.ResetPasswordAsync(user, token, resetPasswordVM.NewPassword);

        if (!identityResult.Succeeded)
        {
            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(resetPasswordVM);
        }

        return RedirectToAction(nameof(Login));
    }


}
