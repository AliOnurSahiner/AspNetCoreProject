using Microsoft.AspNetCore.Identity;

namespace AspNetCoreProject.Entites
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string SurName { get; set; }


    }
}
