using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace School_Project.Models
{
    public partial class SchoolContext : DbContext
    {
        public SchoolContext()
        {
        }

        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Timetable> Timetables { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-S37IP9K\\SQLEXPRESS;Database=School;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Timetable>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Timetable");

                entity.Property(e => e.Weekday).HasMaxLength(50);

                entity.Property(e => e._1)
                    .HasMaxLength(50)
                    .HasColumnName("1");

                entity.Property(e => e._2)
                    .HasMaxLength(50)
                    .HasColumnName("2");

                entity.Property(e => e._3)
                    .HasMaxLength(50)
                    .HasColumnName("3");

                entity.Property(e => e._4)
                    .HasMaxLength(50)
                    .HasColumnName("4");

                entity.Property(e => e._5)
                    .HasMaxLength(50)
                    .HasColumnName("5");

                entity.Property(e => e._6)
                    .HasMaxLength(50)
                    .HasColumnName("6");

                entity.Property(e => e._7)
                    .HasMaxLength(50)
                    .HasColumnName("7");

                entity.Property(e => e._8)
                    .HasMaxLength(50)
                    .HasColumnName("8");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(200);

                entity.Property(e => e.Role).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
