using System.Data.Entity;
using ShopApp.Entities;

namespace ShopApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext() : base()
    {
    }

    public DbSet<Owner> Owners { get; set; }
    public DbSet<OwnerDetails> OwnerDetails { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        // Owner -> Store
        modelBuilder.Entity<Store>()
                    .HasRequired(s => s.Owner)
                    .WithMany(s => s.Store)
                    .HasForeignKey(s => s.OwnerId)
                    .WillCascadeOnDelete(true);

        // Store -> Product
        modelBuilder.Entity<Product>()
                    .HasRequired(p => p.Store)
                    .WithMany(p => p.Product)
                    .HasForeignKey(p => p.StoreId)
                    .WillCascadeOnDelete(true);
    }

    public override int SaveChanges()
    {
        AddTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync()
    {
        AddTimestamps();
        return base.SaveChangesAsync();
    }

    private void AddTimestamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity
                        && (e.State == EntityState.Added || e.State == EntityState.Modified)
            );

        foreach (var entity in entities)
        {
            var now = DateTime.Now;
            var baseEntity = entity.Entity as BaseEntity;

            if (entity.State == EntityState.Added)
            {
                if (baseEntity != null) baseEntity.CreatedAt = now;
            }

            if (baseEntity != null)
                baseEntity.UpdatedAt = now;
        }
    }
}