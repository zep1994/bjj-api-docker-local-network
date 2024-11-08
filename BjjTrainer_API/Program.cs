using BjjTrainer_API.Data;
using BjjTrainer_API.Models.User;
using BjjTrainer_API.Services_API;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configure the DbContext to use PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"));
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();



builder.Services.AddScoped<LessonService>();
builder.Services.AddScoped<LessonSectionService>();
builder.Services.AddScoped<SubLessonService>();
builder.Services.AddTransient<JwtTokenService>();


builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Other middleware and app configuration
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
