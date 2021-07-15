using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RDLinker.Data
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IDbContext DbContext { get; }

        Task<TEntity> FindAsync(string id);

        Task<string> CreateAsync(TEntity entity);

        Task UpdateAsync(TEntity entity, IDictionary<string, string> paramList = null);

        Task<int> DeleteAsync(string id);
    }
}
