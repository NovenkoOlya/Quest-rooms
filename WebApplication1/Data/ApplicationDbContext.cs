using System.Collections.Generic;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }

}
