using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniqueMvc.Models;

namespace UniqueMvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user != null)
            {
                
                //sign in
                var signInResult = await signInManager.PasswordSignInAsync(user.UserName, password, false, false);

                if (signInResult.Succeeded)
                {
                    if (user.Access == "Admin")
                    {
                        return RedirectToAction("Index","Admin", new { area = "" } );
                    }
                    else if(user.Access == "Student")
                    {
                        return RedirectToAction("Index", "Student", new { area = "" });
                    }
                }
            }

            return View();
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(string userName, string password, string name, string address)
        {
            var user = new ApplicationUser
            {
                Name = name,
                Access = "Student",
                HomeAddress = address,
                UserName = userName,
            };

            var pw = password;
            var result = await userManager.CreateAsync(user, password);
            
            if (result.Succeeded)
            {
                user = await userManager.FindByNameAsync(user.UserName);
            }

            return RedirectToAction("Login");
        }


        [Authorize(Roles = "Student")]
        public IActionResult Secret()
        {
            return View();
        }
    }
}
