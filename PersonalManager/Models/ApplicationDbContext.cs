using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace PersonalManager.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Doa> Doas { get; set; }
        public DbSet<DoaText> DoaTexts { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}