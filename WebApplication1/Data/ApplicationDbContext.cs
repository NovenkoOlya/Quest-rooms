using System.Collections.Generic;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Quest> Quest { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Review> Review { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.User_ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Room)
                .WithMany(r => r.Review)
                .HasForeignKey(r => r.Room_ID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
