using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace RDLinker.Core.Data
{
    public interface IDbContext : IDisposable
    {
        IDbConnection DbConnection { get; }

        void OpenDb();

        IDbTransaction BeginDbTransaction();

        void Commit();

        void RollBack();

        IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

        Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction dbTransaction = null);

        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction dbTransaction = null) where T : class;

        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction dbTransaction = null) where T : class;

        Task<object> ExecuteScalarAsync(string sql, object param = null, IDbTransaction dbTransaction = null);

        Task<IDataReader> ExecuteReaderAsync(string sql, object param = null, IDbTransaction dbTransaction = null);

    }
}
