using CinemaTix.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaTix.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Movies> Movies { get; set; }
        public DbSet<Shows> Shows { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Reviews> Reviews { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movies>(entity =>
            {
                entity.Property(m => m.CreatedDate)
                .HasColumnType("timestamp");

                entity.Property(m => m.UpdatedDate)
                .HasColumnType("timestamp");

                entity.Property(m => m.DeletedDate)
                .HasColumnType("timestamp");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(m => m.CreatedDate)
                .HasColumnType("timestamp");

                entity.Property(m => m.UpdatedDate)
                .HasColumnType("timestamp");

                entity.Property(m => m.DeletedDate)
                .HasColumnType("timestamp");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(m => m.CreatedDate)
                .HasColumnType("timestamp");

                entity.Property(m => m.UpdatedDate)
                .HasColumnType("timestamp");

                entity.Property(m => m.DeletedDate)
                .HasColumnType("timestamp");
            });

            modelBuilder.Entity<Shows>(entity =>
            {
                entity.Property(m => m.CreatedDate)
                .HasColumnType("timestamp");

                entity.Property(m => m.UpdatedDate)
                .HasColumnType("timestamp");

                entity.Property(m => m.DeletedDate)
                .HasColumnType("timestamp");

                entity.Property(m => m.Schedule)
                .HasColumnType("timestamp");
            });

            modelBuilder.Entity<Reviews>(entity =>
            {
                entity.Property(m => m.CreatedDate)
                .HasColumnType("timestamp");

                entity.Property(m => m.UpdatedDate)
                .HasColumnType("timestamp");

                entity.Property(m => m.DeletedDate)
                .HasColumnType("timestamp");
            });
        }
    }
}
