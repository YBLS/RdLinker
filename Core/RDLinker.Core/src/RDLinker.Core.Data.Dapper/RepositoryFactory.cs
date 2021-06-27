using RDLinker.Core.Data.Schema;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RDLinker.Core.Data.Dapper
{
    public class RepositoryFactory
    {
        private static ConcurrentDictionary<string, Type> _repositories = new ConcurrentDictionary<string, Type>();

        public static void RegisterRepository<TEntity>() where TEntity : IEntity
        {
            Type type = typeof(TEntity);
            string key = type.Name;
            _repositories.TryAdd(key, type);
        }

        public static IRepository<TEntity> GetRepository<TEntity>(IDbContext context) where TEntity : BaseEntity
        {
            string logicalName = string.Empty;
            string primaryKey = string.Empty;

            Type entity = typeof(TEntity);
            var attrs = entity.GetCustomAttributes(typeof(TableAttribute), false);
            if (attrs?.Length > 0)
            {
                logicalName = (attrs[0] as TableAttribute).Name;
            }

            var props = entity.GetProperties();
            var keys = props.Where(p => p.GetCustomAttributes(typeof(KeyAttribute), false)?.Length > 0);
            if (keys.Any())
            {
                var key = keys.First();
                var customKey = key.GetCustomAttributes(typeof(ColumnAttribute), false);
                if (customKey?.Length > 0)
                {
                    primaryKey = (customKey[0] as ColumnAttribute).Name;
                }
                else
                {
                    primaryKey = key.Name;
                }
            }

            return (IRepository<TEntity>)Activator.CreateInstance(typeof(BaseRepository<TEntity>), context, logicalName, primaryKey);
        }
    }
}
