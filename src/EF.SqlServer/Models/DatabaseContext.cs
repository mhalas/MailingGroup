using Microsoft.EntityFrameworkCore;

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

        public virtual DbSet<Mail> Mail { get; set; }
        public virtual DbSet<MailingGroup> MailingGroups { get; set; }
        public virtual DbSet<SystemUser> SystemUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Mail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.MailingGroupId).HasColumnName("MailingGroupID");

                entity.HasOne(d => d.MailingGroup)
                    .WithMany(p => p.Mail)
                    .HasForeignKey(d => d.MailingGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Mail__MailingGro__286302EC");
            });

            modelBuilder.Entity<MailingGroup>(entity =>
            {
                entity.ToTable("MailingGroup");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.SystemUserId).HasColumnName("SystemUserID");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.MailingGroups)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MailingGr__Syste__25869641");
            });

            modelBuilder.Entity<SystemUser>(entity =>
            {
                entity.ToTable("SystemUser");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Salt)
                    .IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
