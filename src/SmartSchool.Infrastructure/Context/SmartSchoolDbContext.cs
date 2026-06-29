using Microsoft.EntityFrameworkCore;
using SmartSchool.Domain.Entities;

namespace SmartSchool.Infrastructure.Context;

public class SmartSchoolDbContext : DbContext
{
    public SmartSchoolDbContext(DbContextOptions<SmartSchoolDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SmartSchoolDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

}