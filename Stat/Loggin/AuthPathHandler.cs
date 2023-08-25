using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Stats.Loggin
{
    public class AuthPathHandler
    {
        List<Person> people = new List<Person> {
            new Person("wqe@gmail.com","qwewq")
        };
        readonly RequestDelegate next;

        public AuthPathHandler(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            string path = context.Request.Path;
            if (path == "/login" && context.Request.Method == "GET")
            {
                LoginUser(context);
            }
            else if(path == "/login" && context.Request.Method=="POST"){
                var returnUrl = context.Request.Query["returnUrl"].ToString();
                PostLoginUser(context, returnUrl);
            }
            else if(path == "/logout" && context.Request.Method== "GET")
            {
                LogoutUser(context);
            }
        }

        public async Task LoginUser(HttpContext context)
        {
            //await context.Response.WriteAsync("Hello");
            context.Response.ContentType = "text/html; charset=utf-8";
            string loginForm = @"<!DOCTYPE html>
            <html>
            <head>
                <meta charset='utf-8' />
                <title>METANIT.COM</title>
            </head>
            <body>
                <h2>Login Form</h2>
                <form method='post'>
                    <p>
                        <label>Email</label><br />
                        <input name='email' />
                    </p>
                    <p>
                        <label>Password</label><br />
                        <input type='password' name='password' />
                    </p>
                    <input type='submit' value='Login' />
                </form>
            </body>
            </html>";
            await context.Response.WriteAsync(loginForm);
            await next.Invoke(context);
        }
        public async Task<IResult> PostLoginUser(HttpContext context, string? returnUrl)
        {

            var form = context.Request.Form;
            // если email и/или пароль не установлены, посылаем статусный код ошибки 400
            if (!form.ContainsKey("email") || !form.ContainsKey("password"))
                return Results.BadRequest("Email и/или пароль не установлены");

            string email = form["email"];
            string password = form["password"];

            // находим пользователя 
            Person? person = people.FirstOrDefault(p => p.Email == email && p.Password == password);
            // если пользователь не найден, отправляем статусный код 401
            if (person is null) return Results.Unauthorized();

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, person.Email) };
            // создаем объект ClaimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            // установка аутентификационных куки
            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
           // await next.Invoke(context);
            return Results.Redirect(returnUrl ?? "/");
        }
        public async Task<IResult> LogoutUser(HttpContext context)
        {
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            context.Response.Redirect("/login");
            return Results.Redirect("/login");
        }
    }
}
