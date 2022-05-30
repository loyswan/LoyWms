using LoyWms.Application.Common.Interfaces;
using LoyWms.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoyWms.Infrastructure.Persistence.Contexts;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        //foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
        //{
        //    switch (entry.State)
        //    {
        //        case EntityState.Added:
        //            entry.Entity.Created = _dateTime.NowUtc;
        //            entry.Entity.CreatedBy = _authenticatedUser.UserId;
        //            break;
        //        case EntityState.Modified:
        //            entry.Entity.LastModified = _dateTime.NowUtc;
        //            entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
        //            break;
        //    }
        //}
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18,6)");
        }

        base.OnModelCreating(builder);
    }
}
