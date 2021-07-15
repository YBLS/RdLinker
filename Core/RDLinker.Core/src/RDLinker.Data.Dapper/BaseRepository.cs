using RDLinker.Data.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDLinker.Data.Dapper
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        public IDbContext DbContext { get; }
        private string _logicalName { get; }
        private string _primaryKey { get; }
        private DapperDbContext _dbContext
        {
            get
            {
                if (DbContext == null)
                {
                    throw new NullReferenceException("DbContext");
                }
                return DbContext as DapperDbContext;
            }
        }

        public BaseRepository(IDbContext dbContext)
        {
            DbContext = dbContext;

            Type entity = typeof(TEntity);
            var attrs = entity.GetCustomAttributes(typeof(TableAttribute), false);
            if (attrs?.Length > 0)
            {
                _logicalName = (attrs[0] as TableAttribute).Name;
            }

            var props = entity.GetProperties();
            var keys = props.Where(p => p.GetCustomAttributes(typeof(KeyAttribute), false)?.Length > 0);
            if (keys.Any())
            {
                var key = keys.First();
                var customKey = key.GetCustomAttributes(typeof(ColumnAttribute), false);
                if (customKey?.Length > 0)
                {
                    _primaryKey = (customKey[0] as ColumnAttribute).Name;
                }
                else
                {
                    _primaryKey = key.Name;
                }
            }
        }

        public BaseRepository(IDbContext dbContext, string logicalName, string primaryKey)
        {
            DbContext = dbContext;
            _logicalName = logicalName;
            _primaryKey = primaryKey;
        }

        public async Task<string> CreateAsync(TEntity entity)
        {
            Type type = entity.GetType();

            var keyProperty = type.GetProperties().Where(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Length > 0);
            string id = string.Empty;
            if (keyProperty.Any())
            {
                var pi = type.GetProperty(keyProperty.First().Name);
                id = (string)pi.GetValue(entity);
                //没有给主键赋值，提供默认值
                if (string.IsNullOrWhiteSpace(id))
                {
                    id = _dbContext.GeneratorId();
                    pi.SetValue(entity, id);
                }

            }

            entity.CreatedTime = DateTime.Now;
            Type ent = entity.GetType();
            ent.GetCustomAttributes(true);
            StringBuilder sql = _dbContext.GeneratorCreateSql(entity);
            await DbContext.ExecuteAsync(sql.ToString(), entity);
            return id;
        }

        public async Task UpdateAsync(TEntity entity, IDictionary<string, string> paramList = null)
        {
            Type type = entity.GetType();
            //获取主键值
            var keyProperty = type.GetProperties().Where(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Length > 0);
            if (!keyProperty.Any())
            {
                throw new ArgumentNullException("KeyAttribute");
            }
            var pi = type.GetProperty(keyProperty.First().Name);
            string id = (string)pi.GetValue(entity);
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("KeyAttribute Value");
            }

            entity.UpdatedTime = DateTime.Now;
            StringBuilder sql = _dbContext.GeneratorUpdateSql(entity, paramList);

            sql.AppendFormat(" where {0} = @{1}", _primaryKey, pi.Name);

            await DbContext.ExecuteAsync(sql.ToString(), entity);
        }

        public async Task<int> DeleteAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("primarykey value");
            }

            StringBuilder sql = _dbContext.GeneratorDeleteSql(_logicalName, _primaryKey, "@id");

            return await DbContext.ExecuteAsync(sql.ToString(), new { id });
        }

        public async Task<TEntity> FindAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("primarykey value");
            }
            StringBuilder sql = _dbContext.GeneratorSelectSql(typeof(TEntity), _logicalName, _primaryKey, "@id");

            return await DbContext.QueryFirstOrDefaultAsync<TEntity>(sql.ToString(), new { id });
        }
    }
}
