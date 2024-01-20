using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Core.Model;

public partial class DsEcommerceDbContext : DbContext
{
    public DsEcommerceDbContext()
    {
    }

    public DsEcommerceDbContext(DbContextOptions<DsEcommerceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<Commande> Commandes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=DsEcommerce;Username=postgres;Password=22996611");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Codearticle).HasName("pk_article");

            entity.ToTable("article");

            entity.Property(e => e.Codearticle)
                .HasMaxLength(256)
                .HasColumnName("codearticle");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Numcommande)
                .HasMaxLength(255)
                .HasColumnName("numcommande");
            entity.Property(e => e.Prix)
                .HasPrecision(20, 3)
                .HasColumnName("prix");
            entity.Property(e => e.Quantite)
                .HasPrecision(20, 3)
                .HasColumnName("quantite");
        });

        modelBuilder.Entity<Commande>(entity =>
        {
            entity.HasKey(e => e.Numcommande).HasName("pk_commande");

            entity.ToTable("commande");

            entity.Property(e => e.Numcommande)
                .HasMaxLength(256)
                .HasColumnName("numcommande");
            entity.Property(e => e.Datecommande)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datecommande");
            entity.Property(e => e.Datelivraison)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datelivraison");
            entity.Property(e => e.Prixttc)
                .HasPrecision(20, 3)
                .HasColumnName("prixttc");
            entity.Property(e => e.Userid)
                .HasMaxLength(255)
                .HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Commandes)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_commande_user");

            entity.HasMany(d => d.Codearticles).WithMany(p => p.Numcommandes)
                .UsingEntity<Dictionary<string, object>>(
                    "CommandeArticle",
                    r => r.HasOne<Article>().WithMany()
                        .HasForeignKey("Codearticle")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_commande_article_article"),
                    l => l.HasOne<Commande>().WithMany()
                        .HasForeignKey("Numcommande")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_commande_article_commande"),
                    j =>
                    {
                        j.HasKey("Numcommande", "Codearticle").HasName("pk_commande_article");
                        j.ToTable("commandeArticle");
                        j.IndexerProperty<string>("Numcommande")
                            .HasMaxLength(256)
                            .HasColumnName("numcommande");
                        j.IndexerProperty<string>("Codearticle")
                            .HasMaxLength(255)
                            .HasColumnName("codearticle");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("pk_user");

            entity.ToTable("user");

            entity.HasIndex(e => e.Password, "user_password_key").IsUnique();

            entity.Property(e => e.Userid)
                .HasMaxLength(256)
                .HasColumnName("userid");
            entity.Property(e => e.Img)
                .HasMaxLength(255)
                .HasColumnName("img");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
