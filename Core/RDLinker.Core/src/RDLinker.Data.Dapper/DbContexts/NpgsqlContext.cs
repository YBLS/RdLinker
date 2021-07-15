using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RDLinker.Data.Dapper.DbContexts
{
    public class NpgsqlContext : DapperDbContext
    {
        //private IDbConnection _dbConnection = new NpgsqlConnection("");

        public NpgsqlContext()
        {

        }

        protected override IDbConnection CreateDbConnection() => new NpgsqlConnection("Host=172.16.1.20;Port=5432;DataBase=myapp;user id=postgres;password=p@ssw0rd;");

        public override string GeneratorId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
