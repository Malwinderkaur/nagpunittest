using BrokerApp.Data.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BrokerApp.Data.DatabaseContext
{
    public class BrokerDbContext:DbContext
    {
        private static bool _created = false;

        public BrokerDbContext(DbContextOptions<BrokerDbContext> options):base(options)
        {
        }
        public DbSet<Equity> Equity { get; set; }
        public DbSet<User> User { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equity>(entity =>
            {
                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Equity)
                    .HasForeignKey(d => d.HolderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_User");
            });
            //seed data
            modelBuilder.Entity<Equity>().HasData(new Equity { Id = 1, Price = 200 });
            modelBuilder.Entity<User>().HasData(new User { Id=1, Funds=50000});

        }
    }
}
