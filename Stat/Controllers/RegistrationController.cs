using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Stat.DataBase;
using Stat.Models;
using System.Security.Claims;

namespace Stat.Controllers
{
    public class RegistrationController : Controller
    {
        
        [HttpGet]
        public IActionResult RegistrationForm()
        {
            return View();
        }
        [HttpPost]
        public void RegistrationForm(string name, string email, string password)
        {
            ToAddForDb(name, email, password);
        }


        public async Task ToAddForDb(string name, string email, string password)
        {
            using(ApplicationContext db =  new ApplicationContext())
            {
                try
                {
                    var NewUser = new User() { Id = Guid.NewGuid().ToString(), Name = name, Email = email, Password = HashEntryPassword(password) };
                    if (!IsUserRegister(email))
                    {
                        db.Users.Add(NewUser);
                        db.SaveChanges();
                        var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, email) };
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                        HttpContext.Response.Redirect("/");
                    }
                    else
                    {
                        throw new Exception("User is register");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public string HashEntryPassword(string password)
        {
            password = CryptoHelper.Crypto.HashPassword(password);
            return password;
        }

        public bool IsUserRegister(string email)
        {
            bool isReg = false;
            using(ApplicationContext db = new ApplicationContext())
            {
                try
                {
                    isReg = db.Users.Any(x => x.Email == email);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return isReg;
        }
    }
}
