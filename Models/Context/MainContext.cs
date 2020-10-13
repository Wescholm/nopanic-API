using Microsoft.EntityFrameworkCore;

namespace nopanic_API.Models.Context
{
    public class MainDbContext: DbContext
    {
        public DbSet<Test> Test { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<ProfileGradient> ProfileGradients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
        
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            optionsBuilder.UseSqlServer("Server=nopanic-database.c8ba21y7zgcw.eu-central-1.rds.amazonaws.com; Database=dev; User Id=admin; Password=eQY7,bjp!2?4qUh<");
            base.OnConfiguring(optionsBuilder);
            
            // optionsBuilder.UseSqlServer("Server=localhost,1433;Database=nopanic;Trusted_Connection=False;User=sa;Password=Qwerty123;");
            // base.OnConfiguring(optionsBuilder);
        }
    }
}