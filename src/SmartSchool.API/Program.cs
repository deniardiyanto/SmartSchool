using SmartSchool.Application;
using SmartSchool.Infrastructure.DependencyInjection;
using SmartSchool.Infrastructure.Persistence.Context;
using SmartSchool.Infrastructure.Seed;
using SmartSchool.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<SmartSchoolDbContext>();

    await context.Database.MigrateAsync();

    var passwordHasher = services.GetRequiredService<IPasswordHasher>();

    await DataSeeder.SeedAsync(context, passwordHasher);
}

app.Run();
