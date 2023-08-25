using MySqlConnector;
var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddTransient<MySqlConnection>(_ => new MySqlConnection(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Registration}/{action=RegistrationForm}");
app.Run();
