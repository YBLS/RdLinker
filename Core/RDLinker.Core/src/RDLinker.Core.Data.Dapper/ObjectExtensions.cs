using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;
using RDLinker.Core.Data.Schema;
using RDLinker.Core.Data.Dapper.Model;

namespace RDLinker.Core.Data.Dapper
{
    public static class ObjectExtensions
    {
        public static MapPropertiesModel GetMapTableProperties(this Type @this)
        {
            MapPropertiesModel mapPropertiesModel = new MapPropertiesModel();
            
            return new MapPropertiesModel() { TableName = resolveTableName(@this), Properties = resolveProperties(@this) };
        }

        public static IDictionary<string, string> GetMapProperties(this Type @this)
        {
            return resolveProperties(@this);
        }

        public static string GetMapTableName(this Type @this)
        {
            return resolveTableName(@this);
        }

        private static IDictionary<string, string> resolveProperties(Type type)
        {
            var properties = type.GetProperties();
            IDictionary<string, string> props = new Dictionary<string, string>();
            foreach (var item in properties)
            {
                var attr = item.GetCustomAttribute<ColumnAttribute>();
                props.Add(attr?.Name ?? item.Name, item.Name);
            }

            return props;
        }

        private static string resolveTableName(Type type)
        {
            var tableAttr = type.GetCustomAttribute<TableAttribute>();
            string tableName = tableAttr?.Name;
            if (string.IsNullOrWhiteSpace(tableName))
            {
                tableName = type.Name;
            }
            return tableName;
        }
    }
}
