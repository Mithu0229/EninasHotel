﻿using EninasHotel.Application.Common.Interfaces;
using EninasHotel.Application.Common.Utility;
using EninasHotel.Domain.Entities;
using EninasHotel.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EninasHotel.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login(string returnUrl = null)
        {

            returnUrl ??= Url.Content("~/");

            LoginVM loginVM = new()
            {
                RedirectUrl = returnUrl
            };

            return View(loginVM);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager
                    .PasswordSignInAsync(loginVM.Email, loginVM.Password, loginVM.RememberMe, lockoutOnFailure: false);


                if (result.Succeeded)
                {

                    var user = await _userManager.FindByEmailAsync(loginVM.Email);
                    if (await _userManager.IsInRoleAsync(user, SD.Role_Admin))
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }else if (await _userManager.IsInRoleAsync(user, SD.Role_User))
                    {
                        return RedirectToAction("Index", "DashboardACM");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(loginVM.RedirectUrl))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return LocalRedirect(loginVM.RedirectUrl);
                        }
                    }
                        
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }

            return View(loginVM);
        }

        public IActionResult Register(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).Wait();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).Wait();
            }
            RegisterVM registerVM = new()
            {
                RoleList = _roleManager.Roles.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Name
                }),
                RedirectUrl = returnUrl
            };

            return View(registerVM);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new()
                {
                    Name = registerVM.Name,
                    Email = registerVM.Email,
                    PhoneNumber = registerVM.PhoneNumber,
                    NormalizedEmail = registerVM.Email.ToUpper(),
                    EmailConfirmed = true,
                    UserName = registerVM.Email,
                    CreatedAt = DateTime.Now
                };

                var result = await _userManager.CreateAsync(user, registerVM.Password);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(registerVM.Role))
                    {
                        await _userManager.AddToRoleAsync(user, registerVM.Role);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, SD.Role_Customer);
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    if (string.IsNullOrEmpty(registerVM.RedirectUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return LocalRedirect(registerVM.RedirectUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            registerVM.RoleList = _roleManager.Roles.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Name
            });

            return View(registerVM);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            //return RedirectToAction("Index", "Home");
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}