using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Stat.Models
{
    public class User
    {

        public string? Id { get; set; }
        [BindRequired]
        public string? Name { get; set; }
        [BindRequired]
        public string? Email { get; set; }
        [BindRequired]
        public string? Password { get; set; }
        //public User(string name, string email, string password)
        //{
        //    this.Id = Guid.NewGuid().ToString();
        //    Name = name;
        //    Email = email;
        //    Password = password;
        //}
    }
}
