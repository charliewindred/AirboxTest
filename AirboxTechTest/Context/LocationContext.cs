using AirboxTechTest.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace AirboxTechTest.Context
{
    public class LocationContext : DbContext
    {
        /// <remark>
        /// virtual so it can be overriden by the mock that inherits it in the unit test    
        /// </remark>
        public virtual DbSet<Location> Locations {  get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=LocationDB;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.Locations).WithOne("User");
        }
    }
}
