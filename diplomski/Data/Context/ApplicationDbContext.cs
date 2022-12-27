using diplomski.Data.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace diplomski.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Meal> Meals { get; set; }
        public DbSet<UserData> UserDatas { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<AdminDefault> AdminDefaults { get; set; }
        public DbSet<AdminData> AdminDatas { get; set; }
        public DbSet<Foodstuff> Foodstuffs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
