using SmartSchool.Application;
using SmartSchool.Infrastructure.DependencyInjection;
using SmartSchool.Infrastructure.Context;
using SmartSchool.Infrastructure.Seed;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Application.Common.Interfaces;

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
    var context = scope.ServiceProvider.GetRequiredService<SmartSchoolDbContext>();
    var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

    await context.Database.EnsureCreatedAsync();

    await DataSeeder.SeedAsync(context, passwordHasher);
}

app.Run();
