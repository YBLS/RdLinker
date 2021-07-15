using RDLinker.Test.Data.Entity;
using System;
using Xunit;
using RDLinker.Data.Dapper;
using RDLinker.Data.Dapper.DbContexts;

namespace RDLinker.Test.Data
{
    public class UnitTest1
    {
        string id = "2b8574cb-b35f-4553-8cab-403f4d05bbbc";
        [Fact]
        public async void TestFindSql()
        {
            using (var db = new NpgsqlContext())
            {
                var user = await db.Repository<SystemUser>().FindAsync(id);
            }
        }

        [Fact]
        public async void TestSearchSql()
        {
            using (var db = new NpgsqlContext())
            {
                var users = await db.QueryAsync<dynamic>("select systemuserid from systemuser");
                foreach (var item in users)
                {
                    //await db.Repository<SystemUser>().DeleteAsync(item.systemuserid);
                }
            }
        }

        [Fact]
        public async void TestSearchAndDeleteSql()
        {
            using (var db = new NpgsqlContext())
            {
                var users = await db.QueryAsync<dynamic>("select systemuserid from systemuser");
                foreach (var item in users)
                {
                    await db.Repository<SystemUser>().DeleteAsync(item.systemuserid);
                }
            }
        }


        [Fact]
        public void TestUpdateSql()
        {
            using (var db = new NpgsqlContext())
            {
                var user = db.Repository<SystemUser>().FindAsync(id).Result;

                user.Age = 31;

                db.Repository<SystemUser>().UpdateAsync(user);
            }
        }


        [Fact]
        public void TestDeleteSql()
        {
            using (var db = new NpgsqlContext())
            {
                db.Repository<SystemUser>().DeleteAsync(id);
            }
        }

        [Fact]
        public void TestCreateSql()
        {
            Random rnd = new Random();
            using (var db = new NpgsqlContext())
            {
                int len = 1000;
                for (int i = 0; i < len; i++)
                {
                    SystemUser user = new SystemUser()
                    {
                        Name = $"kane{DateTime.Now}",
                        Age = rnd.Next(10, 50)
                    };
                    var result = db.Repository<SystemUser>().CreateAsync(user).Result;
                }
            }
        }

        [Fact]
        public void TestType()
        {
            using (var db = new NpgsqlContext())
            {
                SystemUser user = new SystemUser()
                {
                    Name = $"kane{DateTime.Now}",
                    Age = 30
                };
                int len = 10000;
                for (int i = 0; i < len; i++)
                {
                    Type type = typeof(SystemUser);
                }
            }
        }
    }
}
