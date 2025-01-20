using Microsoft.EntityFrameworkCore;
using ProjektSemestralnyTinWebApi.Models;

namespace ProjektSemestralnyTinWebApi.Context;

public partial class MasterContext : DbContext
{
    public MasterContext()
    {
    }

    public MasterContext(DbContextOptions<MasterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Summit> Summits { get; set; }

    public virtual DbSet<SummitsImage> SummitsImages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<PersonalSummitList> PersonalSummitLists { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("region_pk");

            entity.ToTable("region");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NameEn)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name_en");
            entity.Property(e => e.NamePl)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name_pl");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Review_pk");

            entity.ToTable("Review");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Body)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("body");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.SummitId).HasColumnName("Summit_id");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.Summit).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.SummitId)
                .HasConstraintName("Review_Summit")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Opinion_User");
        });


        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Roles_pk");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<PersonalSummitList>(entity =>
        {
            entity.ToTable("Personal_summit_list");
            entity.HasKey(psl => new { psl.SummitId, psl.UserId });
            
            entity.Property(psl => psl.SummitId).HasColumnName("Summit_id");
            entity.Property(psl => psl.UserId).HasColumnName("User_id");

            entity.HasOne(psl => psl.Summit)
                .WithMany(s => s.PersonalSummitLists)
                .HasForeignKey(psl => psl.SummitId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(psl => psl.User)
                .WithMany(u => u.PersonalSummitLists)
                .HasForeignKey(psl => psl.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Summit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Summit_pk");

            entity.ToTable("Summit");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DescEn)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("desc_en");
            entity.Property(e => e.DescPl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("desc_pl");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.RegionId).HasColumnName("region_id");

            entity.HasOne(d => d.Region).WithMany(p => p.Summits)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Summit_region");
        });

        modelBuilder.Entity<SummitsImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Summits_images_pk");

            entity.ToTable("Summits_images");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("imageUrl");
            entity.Property(e => e.NameEn)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name_en");
            entity.Property(e => e.NamePl)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name_pl");
            entity.Property(e => e.SummitId).HasColumnName("Summit_id");

            entity.HasOne(d => d.Summit).WithMany(p => p.SummitsImages)
                .HasForeignKey(d => d.SummitId)
                .HasConstraintName("Summits_images_Summit")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pk");

            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Login)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(172)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(172)
                .IsUnicode(false)
                .HasColumnName("refreshToken");
            entity.Property(e => e.RefreshTokenExp)
                .HasColumnType("datetime")
                .HasColumnName("refreshTokenExp");
            entity.Property(e => e.RolesId).HasColumnName("Roles_id");
            entity.Property(e => e.Salt)
                .HasMaxLength(172)
                .IsUnicode(false)
                .HasColumnName("salt");

            entity.HasOne(d => d.Roles).WithMany(p => p.Users)
                .HasForeignKey(d => d.RolesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}