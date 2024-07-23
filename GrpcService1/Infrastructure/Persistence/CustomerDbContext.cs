using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using CustomerCustom = GrpcService1.Model.Customer;

namespace GrpcService1.Infrastructure.Persistence;

public class CustomerDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<CustomerCustom> CustomerCustom { get; init; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<CustomerCustom>().ToCollection("customers");
    }
}