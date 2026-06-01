using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TimeManager.Data
{
    public class TimeManagerDbContext : DbContext
    {
        public DbSet<TaskEntity> Tasks => Set<TaskEntity>();
        public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(AppContext.BaseDirectory, "ptm.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryEntity>()
                .HasIndex(c => c.Name)
                .IsUnique();
            modelBuilder.Entity<TaskEntity>()
                .HasOne(t => t.Category)
                .WithMany()
                .HasForeignKey(t => t.CategoryId);
        }
    }
}