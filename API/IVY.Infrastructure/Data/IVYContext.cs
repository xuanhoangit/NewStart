using IVY.Domain.Models;
using IVY.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IVY.Infrastructure.Data;

public class IVYDbContext : IdentityDbContext<EmployeeIdentity,IdentityRole<Guid>,Guid>
{
    public IVYDbContext(DbContextOptions<IVYDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
         // TPT mapping: mỗi loại user nằm ở 1 bảng riêng
        builder.Entity<EmployeeIdentity>().ToTable("Employees");

        builder.Entity<IdentityRole<Guid>>(entity =>
        {
            entity.ToTable("Roles");
        });
        builder.Entity<IdentityUserRole<Guid>>(entity =>
        {
            entity.ToTable("UserRoles");
            entity.Property(x => x.UserId).HasColumnName("UserId");
        });
        builder.Entity<IdentityUserClaim<Guid>>(entity =>
        {
            entity.ToTable("UserClaims");
            entity.Property(x => x.UserId).HasColumnName("UserId");
        });
        builder.Entity<IdentityUserLogin<Guid>>(entity =>
        {
            entity.ToTable("UserLogins");
            entity.Property(x => x.UserId).HasColumnName("UserId");
        });
        builder.Entity<IdentityRoleClaim<Guid>>(entity =>
        {
            entity.ToTable("RoleClaims");
        });
        builder.Entity<IdentityUserToken<Guid>>(entity =>
        {
            entity.ToTable("UserTokens");
            entity.Property(x => x.UserId).HasColumnName("UserId");
        });

    }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductColor> ProductColors { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductCollection> ProductCollections { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Collection> Collections { get; set; }
    public DbSet<Size> Sizes { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<ProductColorFile> ProductColorFiles { get; set; }
    public DbSet<ProductFavorite> ProductFavorites { get; set; }

}
