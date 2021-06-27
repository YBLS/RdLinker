using RDLinker.Test.Data.Entity;
using System;
using Xunit;
using RDLinker.Core.Data.Dapper;
using RDLinker.Core.Data.Dapper.DbContexts;
using System.Data.Common;
using System.Data;
using Npgsql;
using Dapper;

namespace RDLinker.Test.Data
{
    public class DapperTest
    {
        string id = "7de558fc-c7af-4d43-b50a-df6c94f6983b";

        [Fact]
        public void TestDeleteSql()
        {
            using (IDbConnection dbConnection = new NpgsqlConnection("Host=172.16.1.20;Port=5432;DataBase=myapp;user id=postgres;password=p@ssw0rd;"))
            {
                dbConnection.Open();
                var rows = dbConnection.ExecuteAsync(@"delete from systemuser where systemuserid = @id", new { id });
            }

        }
    }
}
