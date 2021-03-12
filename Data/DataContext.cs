using GameWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace GameWeb.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) 
            : base(options)
        {

        }

        public DbSet<Player> Players { get; set; }        

        protected override void OnModelCreating(ModelBuilder builder)
        {
               builder.Entity<Player>().HasKey(m => m.Id);
               base.OnModelCreating(builder);
        }
    }
}