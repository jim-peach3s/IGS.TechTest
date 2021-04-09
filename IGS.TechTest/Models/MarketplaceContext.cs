using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace IGS.TechTest.Models {
  /// <summary>
  /// Generated from EntityFramework
  /// </summary>
  public partial class MarketplaceContext : DbContext {
    public MarketplaceContext() { }

    public MarketplaceContext(DbContextOptions<MarketplaceContext> options)
      : base(options) { }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

      modelBuilder.Entity<Product>(entity => {
        entity.HasKey(e => e.ProductCode);

        entity.Property(e => e.Name).IsUnicode(false);
      });

      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}