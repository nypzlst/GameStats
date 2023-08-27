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
        #region TestConnectionToDb
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
        #endregion
        #region TestCheckToAddUserForDb
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
        #endregion
        #region CheckUserForLogin Test
        [Test]
        public void CheckUserForLogin()
        {
            using(ApplicationContext db =new ApplicationContext())
            {
                var people = new User() { Id = Guid.NewGuid().ToString(), Name = "Tomas", Email = "qweqwq@gmail.com", Password = "sdadasawsewq12" };
                var FounderUser = db.Users.FirstOrDefault(x => x.Email == people.Email);

                if (FounderUser != null && CryptoHelper.Crypto.VerifyHashedPassword(FounderUser.Password, people.Password))
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }
        }
        #endregion
    }
}