using SmartSchool.Domain.Entities;
using SmartSchool.Infrastructure.Context;
using SmartSchool.Application.Common.Interfaces;

namespace SmartSchool.Infrastructure.Seed;

public static class DataSeeder
{
    public static async Task SeedAsync(
        SmartSchoolDbContext context,
        IPasswordHasher passwordHasher)
    {
        if (!context.Roles.Any())
        {
            context.Roles.Add(new Role
            {
                Name = "Admin",
                Description = "System Administrator"
            });

            context.Roles.Add(new Role
            {
                Name = "Officer",
                Description = "Petugas Absensi"
            });

            await context.SaveChangesAsync();
        }

        if (!context.Users.Any())
        {
            var adminRole = context.Roles.First(x => x.Name == "Admin");

            context.Users.Add(new User
            {
                Username = "admin",
                PasswordHash = passwordHasher.Hash("Admin123!"),
                FullName = "Administrator",
                RoleId = adminRole.Id,
                IsActive = true
            });

            await context.SaveChangesAsync();
        }
    }
}