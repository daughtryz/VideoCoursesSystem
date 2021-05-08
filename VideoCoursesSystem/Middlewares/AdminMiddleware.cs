using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoCoursesSystem.Data.Models;

namespace VideoCoursesSystem.Middlewares
{
    public class AdminMiddleware
    {
        private readonly RequestDelegate next;

        public AdminMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<ApplicationUser> userManager)
        {
            await this.SeedUserInRoles(userManager);
            await this.next(context);
        }

        private async Task SeedUserInRoles(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any(x => x.Id == "admin42"))
            {
                var user = new ApplicationUser
                {
                    UserName = "admin42",
                    Email = "admin42@abv.bg",                  
                    Id = "admin42"
                };

                var result = await userManager.CreateAsync(user, "Qwerty1234_");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "ADMIN");
                }
                else
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
