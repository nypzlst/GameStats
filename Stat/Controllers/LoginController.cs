using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Stat.DataBase;
using System.Security.Claims;

namespace Stat.Controllers
{
    public class LoginController : Controller
    {
        ApplicationContext db;
        public LoginController(ApplicationContext context)
        {
            db=context;
        }

        [HttpGet]
        public IActionResult LoginForm()
        {
            return View();
        }

        [HttpPost]
        public void LoginForm(string emailLog, string passwordLog)
        {
            CheckUserInDbAndLogin(emailLog, passwordLog);   
        }
        public async Task CheckUserInDbAndLogin(string emailLog, string passwordLog)
        {
            var FoundUser = db.Users.FirstOrDefault(x => x.Email == emailLog);
            if (FoundUser != null)
            {
                var isCheck = CryptoHelper.Crypto.VerifyHashedPassword(FoundUser.Password, passwordLog);
                if (isCheck)
                {
                    var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, emailLog) };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    HttpContext.Response.Redirect("/");
                }
            }
        }
    }
}
