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
        public DbSet<Account> Accounts { get; set; }
        #endregion

        #region Post
        public DbSet<Post> Posts { get; set; }
        #endregion

        #region Comment
        public DbSet<Comment> Comments { get; set; }
        #endregion

        #region Reaction
        public DbSet<Reaction> Reactions { get; set; }
        #endregion

        #region KeySearch
        public DbSet<KeySearch> KeySearchs { get; set; }
        #endregion

        #region RequestFriend
        public DbSet<RequestFriend> RequestFriends { get; set; }
        #endregion

        #region PushSetting
        public DbSet<PushSetting> PushSettings { get; set; }
        #endregion

        #region Notification
        public DbSet<Notification> Notifications { get; set; }
        #endregion

        #region Friend
        public DbSet<Friend> Friends { get; set; }
        #endregion

        public static void UpdateDatabase(DatabaseContext context)
        {
            context.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var sqlConnection = "data source=DESKTOP-V87NI7H;initial catalog=anti_fakebook;user id=sa;password=1234$;MultipleActiveResultSets=true;";
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