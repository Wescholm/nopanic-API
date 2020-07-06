using Microsoft.EntityFrameworkCore;

namespace nopanic_API.Models.Context
{
    public class MainDbContext: DbContext
    {
        public DbSet<Test> Test { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            optionsBuilder.UseSqlServer("Server=dev-db.c8ba21y7zgcw.eu-central-1.rds.amazonaws.com; Database=dev; User Id=admin; Password=JwKg2xKFVK5D8EYE");
            base.OnConfiguring(optionsBuilder);
        }
    }
}