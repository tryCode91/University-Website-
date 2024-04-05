using Microsoft.AspNetCore.Identity;
using Teaching.Models;

namespace Teaching.Data
{
    public class Seed
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.Student))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Student));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "petrusdughem@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "petrusdughem@gmail.com",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Country = "Sweden",
                            City = "Trollhättan",
                            Street = "Bråvalla 22",
                            PostalCode = "412 32"
                        }
                    };
    
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                
                }

                string appUserEmail = "isp@isp.se";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = appUserEmail,
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Country = "Malta",
                            City = "Mellieha",
                            Street = "25 de Marco Street",
                            PostalCode = "04829-310"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Student);
                }
            }
        }

        public static void SeedStudentAndCourses(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope()) 
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Student.Any())
                {
                    context.Student.AddRange(new List<Student>()
                    {

                        new Student()
                        {
                            EmailAddress = "petrusdughem@gmail.com",
                            Name = "Petrus Dughem",
                            Age = 32,
                            Address = new Address
                            {
                                Country = "Sweden",
                                City = "Trollhättan",
                                Street = "Bråvalla 22",
                                PostalCode = "412 32"
                            }
                            
                        },
                        new Student()
                        {
                            EmailAddress = "isp@isp.se",
                            Name = "Petrus Dughem",
                            Age = 32,
                            Address = new Address
                            {
                                Country = "Brazil",
                                City = "Sao Paulo",
                                Street = "25 de Marco Street",
                                PostalCode = "04829-310"
                            }
                        },

                    });
            
                    context.SaveChanges();
            
                }

                if (!context.Course.Any())
                {
                    context.Course.AddRange(new List<Course>()
                    {

                        new Course()
                        {
                            Name = "Math 101",
                            Start = new DateTime(2024, 3, 18),
                            End = new DateTime(2024, 6, 3),
                        },
                        new Course()
                        {
                            Name = "Biology 101",
                            Start = new DateTime(2024, 4, 20),
                            End = new DateTime(2024, 7, 14),
                        },

                    });

                    context.SaveChanges();

                }
            }
               
        }
    }
}
