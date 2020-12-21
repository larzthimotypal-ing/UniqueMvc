using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniqueMvc.Models;

namespace UniqueMvc.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext( DbContextOptions<AuthDbContext> options) : base(options)
        {

        }
        
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<TermGrade> TermGrades { get; set; }
        public DbSet<QuizOrAssignment> QuizOrAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            string ADMIN_ID = "16446968-4cef-46d6-83c5-d3a00de4f250";
            //create user
            var appUser = new ApplicationUser
            {
                Id = ADMIN_ID,
                Email = "james@gmail.com",
                EmailConfirmed = true,
                Name = "James",
                UserName = "adminJames",
                Access = "Admin",
                NormalizedUserName = "ADMINJAMES",
                NormalizedEmail = "JAMES@GMAIL.COM"
            };
            //set user password
            PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
            appUser.PasswordHash = ph.HashPassword(appUser, "james123");
            //seed user
            builder.Entity<ApplicationUser>().HasData(appUser);
        }
    }

}
