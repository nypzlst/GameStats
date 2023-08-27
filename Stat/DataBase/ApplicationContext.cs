using Microsoft.EntityFrameworkCore;
using Stat.Models;


namespace Stat.DataBase
{
    public class ApplicationContext :DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) :base(options){ }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySql("Server=localhost;User=root;Password=zxcDaitona500@;Database=StatsUser.db;",new MySqlServerVersion(new Version(8,1,0)));
        //}
    }
}
