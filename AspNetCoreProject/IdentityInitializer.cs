using AspNetCoreProject.Entites;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreProject
{
    public  class IdentityInitializer // Bu class bize Microsoft Identity Sayesinde Admin Role Vs oluşturacak.
    {
        public static void CreateAdminUser(UserManager<AppUser> appUser, RoleManager<IdentityRole> roleManager)
        {
            AppUser user = new AppUser
            {
                Name = "Arya",
                SurName = "Şahiner",
                UserName = "arya.sahiner",
            };
            if (appUser.FindByNameAsync("Arya").Result==null)
            {
                var identityResult = appUser.CreateAsync(user,"1").Result;
            }
            if (roleManager.FindByNameAsync("Admin").Result==null)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = "Admin",
                };
                var identityResult = roleManager.CreateAsync(identityRole).Result;
                var userResult =appUser.AddToRoleAsync(user, identityRole.Name).Result; // Admin kullanıcısı oluşturuken nihai yer burasıdır.
            }

        }


    }
}
