

using Web1_Server.Models;
using Microsoft.EntityFrameworkCore;


namespace Web1_Server
{
    public class DB_Context : DbContext
    {

        public DbSet<LoginModel> Logins { get; set; }


        public DB_Context(DbContextOptions<DB_Context> options) : base(options)
        {
            try
            {
               Database.EnsureCreated();
            }
            catch (Exception e) { Console.WriteLine($"Error data base install {e.Message}"); }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LoginModel>().ToTable("Logins");
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
        }
    }
}
