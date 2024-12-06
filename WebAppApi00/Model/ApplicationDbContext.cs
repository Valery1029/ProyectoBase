using Microsoft.EntityFrameworkCore;
namespace WebAppApi00.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; } = null!; 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
                {
                    Id = 1,
                    FirstName = "Admin",
                    Lastname = "System",
                    Username = "admin",
                    Password = "admin",
                    IsActive = true
                }
            );
        }
    }
}

