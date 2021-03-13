using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EF.SqlServer.Models
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EmailAddress> EmailAddress { get; set; }
        public virtual DbSet<MailingGroup> MailingGroups { get; set; }
        public virtual DbSet<SystemUser> SystemUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<EmailAddress>(entity =>
            {
                entity.HasIndex(e => new { e.Value, e.MailingGroupId }, "Unique_EmailAddress")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.MailingGroupId).HasColumnName("MailingGroupID");

                entity.HasOne(d => d.MailingGroup)
                    .WithMany(p => p.Mail)
                    .HasForeignKey(d => d.MailingGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Mail__MailingGro__2B3F6F97");
            });

            modelBuilder.Entity<MailingGroup>(entity =>
            {
                entity.ToTable("MailingGroup");

                entity.HasIndex(e => new { e.Name, e.SystemUserId }, "Unique_MailingGroup")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.SystemUserId).HasColumnName("SystemUserID");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.MailingGroups)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MailingGr__Syste__276EDEB3");
            });

            modelBuilder.Entity<SystemUser>(entity =>
            {
                entity.ToTable("SystemUser");

                entity.HasIndex(e => e.Username, "UQ__SystemUs__536C85E484561248")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.Salt).IsRequired();

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
