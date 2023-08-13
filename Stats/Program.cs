using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Stats;
using Stats.Loggin;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => options.LoginPath = "/login");
builder.Services.AddAuthorization();
var app = builder.Build();

//app.UseAuthentication();   // добавление middleware аутентификации 
//app.UseAuthorization();
app.UseMiddleware<AuthPathHandler>();


app.Map("/", [Authorize] () => $"Hello World!");

app.Run();

//app.MapGet("/", () => "Hello World!");

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => options.LoginPath = "/login");

