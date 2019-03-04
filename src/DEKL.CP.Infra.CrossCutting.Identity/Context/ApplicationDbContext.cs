using DEKL.CP.Infra.CrossCutting.Identity.Maps;
using DEKL.CP.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace DEKL.CP.Infra.CrossCutting.Identity.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole,
        ApplicationUserClaim>
    {
        public ApplicationDbContext() : base("DEKLCPConnIdentity")
        { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRole { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Properties<string>()
                .Configure(e => e.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

            modelBuilder.Configurations.Add(new ApplicationUserMap());
            modelBuilder.Configurations.Add(new ClientMap());
            modelBuilder.Entity<ApplicationRole>().ToTable(nameof(ApplicationRole));
            modelBuilder.Entity<ApplicationUserRole>().ToTable(nameof(ApplicationUserRole));
            modelBuilder.Entity<ApplicationUserLogin>().ToTable(nameof(ApplicationUserLogin));
            modelBuilder.Entity<ApplicationUserClaim>().ToTable(nameof(ApplicationUserClaim));
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}