using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Service
{
    public interface IServiceBase<TEntity, TPrimaryKey> where TEntity : DomainEntity<TPrimaryKey>
    {
        void Add(TEntity entity);
       
        void Update(TEntity entity);

        void Delete(TPrimaryKey id);

        TEntity GetById(TPrimaryKey id);

        List<TEntity> GetAll();

        List<TEntity> GetAllPaging(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize, ref int totalRow);

        Task<List<TEntity>> GetAllAsync(string filter);

        Task<List<TEntity>> GetListCondition(Expression<Func<TEntity, bool>> predicate);

        void Save();
    }
}
