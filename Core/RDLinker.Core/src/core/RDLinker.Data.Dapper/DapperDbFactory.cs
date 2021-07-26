using RDLinker.Data.Dapper.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace RDLinker.Data.Dapper
{
    public class DapperDbFactory
    {
        public static DapperDbContext CreateDbContext()
        {
            return new NpgsqlContext();
        }

    }
}
