using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using RDLinker.Data.Dapper.Model;

namespace RDLinker.Data.Dapper
{
    public abstract class DapperDbContext : IDbContext
    {
        private IDbConnection _dbConnection = null;
        public IDbConnection DbConnection
        {
            get
            {
                if (_dbConnection == null)
                {
                    _dbConnection = CreateDbConnection();
                }
                return _dbConnection;
            }
        }

        public abstract string GeneratorId();

        public DapperDbContext()
        {

        }


        //public DapperDbContext(IDbConnection dbConnection)
        //{
        //    DbConnection = dbConnection;
        //}

        protected abstract IDbConnection CreateDbConnection();

        public IDbTransaction BeginDbTransaction()
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (DbConnection != null)
            {
                DbConnection.Close();
                DbConnection.Dispose();
            }
        }

        public void OpenDb()
        {
            if (DbConnection.State != ConnectionState.Open)
            {
                DbConnection.Open();
            }
        }

        public void CloseDb()
        {
            if (DbConnection.State == ConnectionState.Open)
            {
                DbConnection.Close();
            }
        }

        public void RollBack()
        {
            throw new NotImplementedException();
        }

        public virtual IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            return RepositoryFactory.GetRepository<TEntity>(this);
        }

        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction dbTransaction = null)
        {
            try
            {
                OpenDb();

                return await DbConnection.ExecuteAsync(sql, param);
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                CloseDb();
            }
        }

        public virtual async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction dbTransaction = null) where T : class
        {
            return await DbConnection.QueryAsync<T>(sql, param, dbTransaction);
        }

        public virtual async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction dbTransaction = null) where T : class
        {
            return await DbConnection.QueryFirstOrDefaultAsync<T>(sql, param, dbTransaction);
        }

        public virtual async Task<object> ExecuteScalarAsync(string sql, object param = null, IDbTransaction dbTransaction = null)
        {
            return await DbConnection.ExecuteScalarAsync(sql, param, dbTransaction);
        }

        public virtual async Task<IDataReader> ExecuteReaderAsync(string sql, object param = null, IDbTransaction dbTransaction = null)
        {
            return await DbConnection.ExecuteReaderAsync(sql, param, dbTransaction);
        }


        #region Sql 生成器

        public virtual StringBuilder GeneratorSelectSql(Type type, string logicalName)
        {
            MapPropertiesModel tableProps = type.GetMapTableProperties();

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select {0} from {1}", string.Join(',', tableProps.Properties.Select(p => $"{p.Key} as {p.Value}")), tableProps.TableName);

            return sb;
        }

        public virtual StringBuilder GeneratorSelectSql(Type type, string logicalName, string primaryKey, string primaryKeyValue)
        {
            MapPropertiesModel tableProps = type.GetMapTableProperties();

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select {0} from {1}", string.Join(',', tableProps.Properties.Select(p => $"{p.Key} as {p.Value}")), tableProps.TableName);
            sb.AppendFormat(" where {0}={1}", primaryKey, primaryKeyValue);

            return sb;
        }

        public virtual StringBuilder GeneratorCreateSql(object obj)
        {
            MapPropertiesModel tableProps = obj.GetType().GetMapTableProperties();

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("insert into {0}", tableProps.TableName);
            sb.AppendFormat(" ({0}) ", string.Join(',', tableProps.Properties.Keys));
            sb.AppendFormat(" values ({0}) ", string.Join(',', tableProps.Properties.Values.Select(p => $"@{p}")));
            return sb;
        }

        public virtual StringBuilder GeneratorUpdateSql(object obj, IDictionary<string, string> paramList)
        {
            if (paramList == null)
            {
                paramList = obj.GetType().GetMapProperties();
            }

            var tableName = obj.GetType().GetMapTableName();

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("update {0} set ", tableName);
            var props = paramList.Select(p => $"{p.Key}=@{p.Value}");
            sb.AppendFormat(string.Join(',', props));

            return sb;
        }

        public virtual StringBuilder GeneratorDeleteSql(string logicalName, string primaryKey, string primaryKeyValue)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"delete from {logicalName} where {primaryKey} = {primaryKeyValue}");
            return sb;
        }
        #endregion
    }
}
