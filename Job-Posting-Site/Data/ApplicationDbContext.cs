using Job_Posting_Site.Data.Entities;
using Job_Posting_Site.Data.Entities.Account;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Job_Posting_Site.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private bool seedDb = true;
        private Role role{ get; set; }
        private Category category{ get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (seedDb)
            {
                modelBuilder
                    .Entity<Role>()
                    .HasData(
                        new Role()
                        {
                            Id = 1,
                            Name = "employer"
                        },
                        new Role()
                        {
                            Id = 2,
                            Name = "candidate"
                        }
                        );
                modelBuilder
                    .Entity<Category>()
                    .HasData(
                    new Category() {
                        Id = -1,
                        IsDeleted = false,
                        Name = "QA"
                    },
                    new Category()
                    {
                        Id = -2,
                        IsDeleted = false,
                        Name = "Developer"
                    },
                    new Category()
                    {
                        Id = -3,
                        IsDeleted = false,
                        Name = "Manager"
                    },
                    new Category()
                    {
                        Id = -4,
                        IsDeleted = false,
                        Name = "DevOps"
                    },
                    new Category()
                    {
                        Id = -5,
                        IsDeleted = false,
                        Name = "PM"
                    }
                    );

            }
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}