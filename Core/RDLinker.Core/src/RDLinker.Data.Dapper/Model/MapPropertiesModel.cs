using System;
using System.Collections.Generic;
using System.Text;

namespace RDLinker.Data.Dapper.Model
{
    public class MapPropertiesModel
    {
        public string TableName { get; set; }

        public IDictionary<string, string> Properties { get; set; }
    }
}
