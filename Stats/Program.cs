using Stats.LifeTime;
using Stats.TimeMiddleware;

var builder = WebApplication.CreateBuilder();
builder.Services.AddTransient<ITime, DefaultTime>();

var app = builder.Build();


app.UseMiddleware<TimeMessageMiddleware>();

app.Run();


