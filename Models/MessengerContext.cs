using System;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Messenger.Models
{
    public partial class MessengerContext : DbContext
    {
        public MessengerContext()
        {
        }

        public MessengerContext(DbContextOptions<MessengerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(Startup.Configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("pgcrypto");

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("messages");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Msg)
                    .IsRequired()
                    .HasColumnName("msg");

                entity.Property(e => e.Sent)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("sent");

                entity.Property(e => e.Un)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("un");

                entity.HasOne(d => d.UnNavigation)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.Un)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("messages_fk");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Un)
                    .HasName("users_pk");

                entity.ToTable("users");

                entity.Property(e => e.Un)
                    .HasMaxLength(50)
                    .HasColumnName("un");

                entity.Property(e => e.Pw)
                    .IsRequired()
                    .HasColumnName("pw");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}