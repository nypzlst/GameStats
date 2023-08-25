namespace Stats
{
    public class Person
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public Person (string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }
    }
}
