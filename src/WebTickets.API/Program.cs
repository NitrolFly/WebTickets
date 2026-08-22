using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebTickets.Application.Abstractions.Security;
using WebTickets.Application.Services;
using WebTickets.Infrastructure;
using WebTickets.Infrastructure.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSingleton<IPasswordHasher, Argon2PasswordHasher>();
builder.Services.AddScoped<UserRegistrationService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
    options.UseSnakeCaseNamingConvention();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();