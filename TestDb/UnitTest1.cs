using MySqlConnector;
using Stat.DataBase;
using Stat.Models;
using System.Data;

namespace TestDb
{
    public class Tests
    {
        private string connectionString;
        [SetUp]
        public void Setup()
        {
            connectionString = "Server=localhost;User=root;Password=zxcDaitona500@;Database=StatsUser.db";
        }

        [Test]
        public void TestConnectionToDB()
        {
            using(MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    Assert.AreEqual(ConnectionState.Open, con.State);
                }
                catch (Exception ex)
                {
                    Assert.Fail(ex.Message);
                }
                finally { con.Close(); }
            }
        }
        [Test]
        public void TestToTryUserForDb()
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                try
                {
                    var people = new User() { Id=Guid.NewGuid().ToString(),Name="Tomas", Email = "qweqwq@gmail.com", Password=CryptoHelper.Crypto.HashPassword("sdadasawsewq12") };
                    bool isEmailAllready = db.Users.Any(x => x.Email == people.Email);
                    if(!isEmailAllready)
                    {
                        db.Users.Add(people);
                        db.SaveChanges();
                        Assert.IsTrue(db.Users.Contains(people));
                    }
                    else
                    {
                        Assert.Fail("User allready register");
                    }
                }
                catch(Exception ex)
                {
                    Assert.Fail(ex.Message);
                }
            }
        }
    }
}