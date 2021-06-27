using RDLinker.Core.Data.Dapper.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace RDLinker.Core.Data.Dapper
{
    public class DapperDbFactory
    {
        public static DapperDbContext CreateDbContext()
        {
            return new NpgsqlContext();
        }

    }
}
