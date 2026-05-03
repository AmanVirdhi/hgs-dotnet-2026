using Microsoft.EntityFrameworkCore;
using HgsApi.Models;

namespace HgsApi.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<HgsInfo> HgsInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //USER TABLE
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Username).HasColumnName("username");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Password).HasColumnName("password");

                entity.HasIndex(e => e.Email).IsUnique();
            });

            //HGS INFO TABLE
            modelBuilder.Entity<HgsInfo>(entity =>
            {
                entity.ToTable("hgsinfos");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Grievancetypes).HasColumnName("grievancetypes");
                entity.Property(e => e.Room).HasColumnName("room");
                entity.Property(e => e.Course).HasColumnName("course");
                entity.Property(e => e.Mobile).HasColumnName("mobile");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.UserEmail).HasColumnName("useremail");
            });
        }
    }
}