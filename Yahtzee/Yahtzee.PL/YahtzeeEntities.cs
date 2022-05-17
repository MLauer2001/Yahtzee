using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Yahtzee.PL
{
    public partial class YahtzeeEntities : DbContext
    {
        public YahtzeeEntities()
        {
        }

        public YahtzeeEntities(DbContextOptions<YahtzeeEntities> options)
            : base(options)
        {
           
        }

        public virtual DbSet<tblActivation> tblActivations { get; set; }
        public virtual DbSet<tblLobby> tblLobbies { get; set; }
        public virtual DbSet<tblScorecard> tblScorecards { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }
        public virtual DbSet<tblUserLobby> tblUserLobbies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Yahtzee.DB;Integrated Security=true");
                optionsBuilder.UseSqlServer("Data Source=lauerdb.database.windows.net;Initial Catalog=Yahtzee;User Id=lauerdb;Password=Test123!");
                optionsBuilder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblActivation>(entity =>
            {
                entity.ToTable("tblActivation");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.KeyCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<tblLobby>(entity =>
            {
                entity.ToTable("tblLobby");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.LobbyName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<tblScorecard>(entity =>
            {
                entity.ToTable("tblScorecard");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<tblUser>(entity =>
            {
                entity.ToTable("tblUser");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<tblUserLobby>(entity =>
            {
                entity.ToTable("tblUserLobby");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<spGetActivationsResult>().HasNoKey();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
