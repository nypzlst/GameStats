using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Stat.DataBase;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseMySql("Server=localhost;User=root;Password=zxcDaitona500@;Database=StatsUser.db;", new MySqlServerVersion(new Version(8, 1, 0)));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => options.LoginPath= "/Login/LoginForm");
builder.Services.AddAuthorization();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=LoginForm}");
app.Map("/", [Authorize] () => $"Welcome to main page");

app.Run();
