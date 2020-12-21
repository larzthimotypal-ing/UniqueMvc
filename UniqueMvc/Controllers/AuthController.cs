using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniqueMvc.Data;
using UniqueMvc.Models;

namespace UniqueMvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthDbContext authDbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AuthDbContext authDbContext)
        {
            this.authDbContext = authDbContext;
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
        public async Task<IActionResult> Register(string userName, string password, string name, string homeAddress)
        {
            var user = new ApplicationUser
            {
                Name = name,
                Access = "Student",
                HomeAddress = homeAddress,
                UserName = userName,
            };

            var pw = password;
            var result = await userManager.CreateAsync(user, password);
            
            if (result.Succeeded)
            {
                user = await userManager.FindByNameAsync(user.UserName);

                var prelim = InitializeGrades("Prelim");
                var midterm = InitializeGrades("Midterm");
                var preFinal = InitializeGrades("Prefinal");
                var final = InitializeGrades("Final");
                await authDbContext.AddRangeAsync(prelim, midterm, preFinal, final);
                await authDbContext.SaveChangesAsync();

                var grade = new Grade
                {
                    AppUserID = user.Id,
                    PrelimID = prelim.ID,
                    MidtermID = midterm.ID,
                    PrefinalID = preFinal.ID,
                    FinalID = final.ID
                };

                await  authDbContext.AddAsync(grade);
                await authDbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index","Admin", new { area = "" } );
        }

        public TermGrade InitializeGrades(string term)
        {
            var termGrade = new TermGrade
            {
                Term = term,
                Grade = 0
            };
            string[] types = { "Quiz1", "Quiz2", "Quiz3", "Assignment1", "Assignment2", "Assignment3" };

            var item = new QuizOrAssignment();
            for (int i = 0; i < types.Length; i++)
            {
                item = new QuizOrAssignment
                {
                    Grade = 0,
                    Type = types[i]
                };

                authDbContext.Add(item);
                authDbContext.SaveChanges();

                switch (i)
                {
                    case 0:
                        termGrade.Quiz1ID = item.ID;
                        break;
                    case 1:
                        termGrade.Quiz2ID = item.ID;
                        break;
                    case 2:
                        termGrade.Quiz3ID = item.ID;
                        break;
                    case 3:
                        termGrade.Assignment1ID = item.ID;
                        break;
                    case 4:
                        termGrade.Assignment2ID = item.ID;
                        break;
                    case 5:
                        termGrade.Assignment3ID = item.ID;
                        break;
                    default:
                        break;
                }

            }

            return termGrade;
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
