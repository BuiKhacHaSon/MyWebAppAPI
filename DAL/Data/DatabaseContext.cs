using System;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public virtual DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>().HasData(new Message(){
                Id = 1,
                Name = "Son Dep Trai",
                BodyMessage = "Ok it works ^^",
                CreatedAt = DateTime.UtcNow
            });   
        }
    }
}