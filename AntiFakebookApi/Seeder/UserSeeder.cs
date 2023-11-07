using Microsoft.EntityFrameworkCore;
using AntiFakebookApi.Models;
using AntiFakebookApi.Utility;

namespace AntiFakebookApi.Seeder
{
    class UserSeeder
    {
        private readonly ModelBuilder _modelBuilder;
        public UserSeeder(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        /// <summary>
        /// Excute data
        /// </summary>
        public void SeedData()
        {
            _modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    Id = 1,
                    Email = "Admin@gmail.com",
                    Password = UtilityFunction.CreateMD5("Admin@gmail.com"),
                    Name = "Admin",
                    Avatar = "",
                    BlockedAccountIdList = "",
                    Status = 1,
                }
                );
        }
    }
}
