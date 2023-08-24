using System;
using System.Collections.Generic;
using BackendApi2.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BackendApi2.Entities
{
    //dotnet ef dbcontext scaffold "Server=INHYDDIS1407WTX\SQLEXPRESS;Database=NTTDATA_AK;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Entities\Models
     public partial class RepositoryContext : DbContext
    {
        public RepositoryContext()
        {
            
        }

        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Skill> Skills { get; set; } = null!;

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     if (!optionsBuilder.IsConfigured)
        //     {
        //        optionsBuilder.UseSqlServer("Server=INHYDDIS1407WTX\\SQLEXPRESS;Database=NTTDATA_AK;Trusted_Connection=True;");
        //     }
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Confirmemail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CONFIRMEMAIL");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FULLNAME");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.ToTable("SKILLS");

                entity.Property(e => e.SkillId).HasColumnName("SKILL_ID");

                entity.Property(e => e.EmpId).HasColumnName("EMP_ID");

                entity.Property(e => e.Experience).HasColumnName("EXPERIENCE");

                entity.Property(e => e.Proficiency)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PROFICIENCY");

                entity.Property(e => e.Skillname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SKILLNAME");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.Skills)
                    .HasForeignKey(d => d.EmpId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__SKILLS__EMP_ID__3F466844");
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
