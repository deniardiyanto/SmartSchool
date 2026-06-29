using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using SmartSchool.Domain.Entities;
using SmartSchool.Infrastructure.Context;

namespace SmartSchool.Infrastructure.Seed;

public static class DataSeeder
{
    public static async Task SeedAsync(SmartSchoolDbContext context)
    {
        await context.Database.MigrateAsync();

        // Seed Role
        if (!await context.Roles.AnyAsync())
        {
            var adminRole = new Role
            {
                Name = "ADMIN",
                Description = "Administrator"
            };

            var petugasRole = new Role
            {
                Name = "PETUGAS",
                Description = "Petugas Absensi"
            };

            context.Roles.AddRange(adminRole, petugasRole);
            await context.SaveChangesAsync();
        }

        // Seed Admin
        if (!await context.Users.AnyAsync())
        {
            var adminRole = await context.Roles.FirstAsync(x => x.Name == "ADMIN");

            context.Users.Add(new User
            {
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                FullName = "System Administrator",
                RoleId = adminRole.Id,
                IsActive = true
            });

            await context.SaveChangesAsync();
        }
    }
}