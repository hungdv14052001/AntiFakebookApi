using AntiFakebookApi.Models;
using AntiFakebookApi.Seeder;
using Microsoft.EntityFrameworkCore;

namespace AntiFakebookApi.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }


        #region Account
        public DbSet<Account> accounts { get; set; }
        #endregion

        public static void UpdateDatabase(DatabaseContext context)
        {
            context.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var sqlConnection = "data source=DESKTOP-LT577PM\\SQLEXPRESS;initial catalog=anti_fakebook;user id=sa;password=1234$;MultipleActiveResultSets=true;";
                optionsBuilder.UseSqlServer(sqlConnection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User
            new UserSeeder(modelBuilder).SeedData();
            #endregion

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}