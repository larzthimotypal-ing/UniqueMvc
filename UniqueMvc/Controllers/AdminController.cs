﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniqueMvc.Data;
using UniqueMvc.Models;

namespace UniqueMvc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly AuthDbContext authDbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AdminController(AuthDbContext authDbContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.authDbContext = authDbContext;
            this.userManager = userManager;
            this.signInManager = signInManager;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyStudents()
        {

            return View(authDbContext.ApplicationUsers.ToList());
        }
        
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await userManager.FindByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await authDbContext.ApplicationUsers.FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }


            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Deleted(string id)
        {
            var student = await authDbContext.ApplicationUsers.FindAsync(id);
            authDbContext.ApplicationUsers.Remove(student);
            await authDbContext.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await userManager.FindByIdAsync(id);
            if (id == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, ApplicationUser student)
        {

            var currentAcc = await userManager.FindByIdAsync(id);
            if (ModelState.IsValid)
            {
                currentAcc.Name = student.Name;
                currentAcc.HomeAddress = student.HomeAddress;

                authDbContext.Update(currentAcc);
                await authDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(student);

        }

        public async Task<IActionResult> Grade(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await authDbContext.Grades.FirstOrDefaultAsync(i => i.AppUserID == id);
            if (id == null)
            {
                return NotFound();
            }
            //instantiating objects of properties in Grade Model
            //finding grade fk = termgrade pk
            grade.Prelim = await authDbContext.TermGrades.FindAsync(grade.PrelimID);
            grade.Prelim.Quiz1 = await authDbContext.QuizOrAssignments.FindAsync(grade.Prelim.Quiz1ID);
            grade.Prelim.Quiz2 = await authDbContext.QuizOrAssignments.FindAsync(grade.Prelim.Quiz2ID);
            grade.Prelim.Quiz3 = await authDbContext.QuizOrAssignments.FindAsync(grade.Prelim.Quiz3ID);
            grade.Prelim.Assignment1 = await authDbContext.QuizOrAssignments.FindAsync(grade.Prelim.Assignment1ID);
            grade.Prelim.Assignment2 = await authDbContext.QuizOrAssignments.FindAsync(grade.Prelim.Assignment2ID);
            grade.Prelim.Assignment3 = await authDbContext.QuizOrAssignments.FindAsync(grade.Prelim.Assignment3ID);
            var pQ1 = grade.Prelim.Quiz1.Grade;
            var pQ2 = grade.Prelim.Quiz2.Grade;
            var pQ3 = grade.Prelim.Quiz3.Grade;
            var pA1 = grade.Prelim.Assignment1.Grade;
            var pA2 = grade.Prelim.Assignment2.Grade;
            var pA3 = grade.Prelim.Assignment3.Grade;

            grade.Prelim.Grade = Math.Round(((pQ1 + pQ2 + pQ3 + pA1 + pA2 + pA3) / 6), 2);

            grade.Midterm = await authDbContext.TermGrades.FindAsync(grade.MidtermID);
            grade.Midterm.Quiz1 = await authDbContext.QuizOrAssignments.FindAsync(grade.Midterm.Quiz1ID);
            grade.Midterm.Quiz2 = await authDbContext.QuizOrAssignments.FindAsync(grade.Midterm.Quiz2ID);
            grade.Midterm.Quiz3 = await authDbContext.QuizOrAssignments.FindAsync(grade.Midterm.Quiz3ID);
            grade.Midterm.Assignment1 = await authDbContext.QuizOrAssignments.FindAsync(grade.Midterm.Assignment1ID);
            grade.Midterm.Assignment2 = await authDbContext.QuizOrAssignments.FindAsync(grade.Midterm.Assignment2ID);
            grade.Midterm.Assignment3 = await authDbContext.QuizOrAssignments.FindAsync(grade.Midterm.Assignment3ID);

            var mQ1 = grade.Midterm.Quiz1.Grade;
            var mQ2 = grade.Midterm.Quiz2.Grade;
            var mQ3 = grade.Midterm.Quiz3.Grade;
            var mA1 = grade.Midterm.Assignment1.Grade;
            var mA2 = grade.Midterm.Assignment2.Grade;
            var mA3 = grade.Midterm.Assignment3.Grade;

            grade.Midterm.Grade = Math.Round(((mQ1 + mQ2 + mQ3 + mA1 + mA2 + mA3) / 6), 2);

            grade.Prefinal = await authDbContext.TermGrades.FindAsync(grade.PrefinalID);
            grade.Prefinal.Quiz1 = await authDbContext.QuizOrAssignments.FindAsync(grade.Prefinal.Quiz1ID);
            grade.Prefinal.Quiz2 = await authDbContext.QuizOrAssignments.FindAsync(grade.Prefinal.Quiz2ID);
            grade.Prefinal.Quiz3 = await authDbContext.QuizOrAssignments.FindAsync(grade.Prefinal.Quiz3ID);
            grade.Prefinal.Assignment1 = await authDbContext.QuizOrAssignments.FindAsync(grade.Prefinal.Assignment1ID);
            grade.Prefinal.Assignment2 = await authDbContext.QuizOrAssignments.FindAsync(grade.Prefinal.Assignment2ID);
            grade.Prefinal.Assignment3 = await authDbContext.QuizOrAssignments.FindAsync(grade.Prefinal.Assignment3ID);

            var rQ1 = grade.Prefinal.Quiz1.Grade;
            var rQ2 = grade.Prefinal.Quiz2.Grade;
            var rQ3 = grade.Prefinal.Quiz3.Grade;
            var rA1 = grade.Prefinal.Assignment1.Grade;
            var rA2 = grade.Prefinal.Assignment2.Grade;
            var rA3 = grade.Prefinal.Assignment3.Grade;

            grade.Prefinal.Grade = Math.Round(((rQ1 + rQ2 + rQ3 + rA1 + rA2 + rA3) / 6), 2);

            grade.Final = await authDbContext.TermGrades.FindAsync(grade.FinalID);
            grade.Final.Quiz1 = await authDbContext.QuizOrAssignments.FindAsync(grade.Final.Quiz1ID);
            grade.Final.Quiz2 = await authDbContext.QuizOrAssignments.FindAsync(grade.Final.Quiz2ID);
            grade.Final.Quiz3 = await authDbContext.QuizOrAssignments.FindAsync(grade.Final.Quiz3ID);
            grade.Final.Assignment1 = await authDbContext.QuizOrAssignments.FindAsync(grade.Final.Assignment1ID);
            grade.Final.Assignment2 = await authDbContext.QuizOrAssignments.FindAsync(grade.Final.Assignment2ID);
            grade.Final.Assignment3 = await authDbContext.QuizOrAssignments.FindAsync(grade.Final.Assignment3ID);

            var fQ1 = grade.Final.Quiz1.Grade;
            var fQ2 = grade.Final.Quiz2.Grade;
            var fQ3 = grade.Final.Quiz3.Grade;
            var fA1 = grade.Final.Assignment1.Grade;
            var fA2 = grade.Final.Assignment2.Grade;
            var fA3 = grade.Final.Assignment3.Grade;

            grade.Final.Grade = Math.Round(((fQ1 + fQ2 + fQ3 + fA1 + fA2 + fA3) / 6), 2);

            var pGrade = grade.Prelim.Grade;
            var mGrade = grade.Midterm.Grade;
            var rGrade = grade.Prefinal.Grade;
            var fGrade = grade.Final.Grade;

            grade.SubjectGrade = Math.Round(((pGrade + mGrade + rGrade + fGrade) / 4), 2);
            return View(grade);
        }

        public async Task<IActionResult> AddGrade(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var itemScore = await authDbContext.QuizOrAssignments.FindAsync(id);
            if (id == null)
            {
                return NotFound();
            }

            return View(itemScore);
        }

        [HttpPost]
        public async Task<IActionResult> AddGrade(int id, QuizOrAssignment qOrA)
        {
            if (id != qOrA.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                if (qOrA.Type == "Quiz1")
                {
                    var term = await authDbContext.TermGrades.FirstOrDefaultAsync(
                            i => i.Quiz1ID == qOrA.ID);
                    authDbContext.Update(term);
                }
                else if (qOrA.Type == "Quiz2")
                {
                    var term = await authDbContext.TermGrades.FirstOrDefaultAsync(
                            i => i.Quiz2ID == qOrA.ID);
                    authDbContext.Update(term);
                }
                else if (qOrA.Type == "Quiz3")
                {
                    var term = await authDbContext.TermGrades.FirstOrDefaultAsync(
                            i => i.Quiz3ID == qOrA.ID);
                    authDbContext.Update(term);
                }
                else if (qOrA.Type == "Assignment1")
                {
                    var term = await authDbContext.TermGrades.FirstOrDefaultAsync(
                            i => i.Assignment1ID == qOrA.ID);
                    authDbContext.Update(term);
                }
                else if (qOrA.Type == "Assignment2")
                {
                    var term = await authDbContext.TermGrades.FirstOrDefaultAsync(
                            i => i.Assignment2ID == qOrA.ID);
                    authDbContext.Update(term);
                }
                else if (qOrA.Type == "Assignment3")
                {
                    var term = await authDbContext.TermGrades.FirstOrDefaultAsync(
                            i => i.Assignment3ID == qOrA.ID);
                    authDbContext.Update(term);
                }

                authDbContext.Update(qOrA);
                await authDbContext.SaveChangesAsync();

                return RedirectToAction("MyStudents");
            }

            return View();

        }


    }
}
