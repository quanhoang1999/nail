using Cms.Data.EF;
using Cms.Infrastructure.Interfaces;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Service
{
    public class ServiceBase<TEntity, TPrimaryKey> : IServiceBase<TEntity, TPrimaryKey>
         where TEntity : DomainEntity<TPrimaryKey>
    {
        private readonly IRepository<TEntity, TPrimaryKey> _repository;
        private readonly IUnitOfWork _unitOfWork;
       
        public ServiceBase(IRepository<TEntity, TPrimaryKey> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
           
        }

        
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.FirstOrDefault(predicate);
        }

        public TEntity FirstOrDefault(TPrimaryKey id)
        {
            return _repository.FirstOrDefault(x => x.Id.Equals(id));
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.FirstOrDefaultAsync(predicate);
        }

       
        public virtual void Add(TEntity entity)
        {
            _repository.Insert(entity);
        }
        
        public virtual void SetUnique(string[] keyfields)
        {
            _repository.SetUnique(keyfields);
        }
      
        public virtual void Delete(TPrimaryKey id)
        {
            _repository.Delete(id);
        }

        public virtual List<TEntity> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public virtual async Task<List<TEntity>> GetAllAsync(string filter)
        {
            return await _repository.GetAllListAsync();
        }

        public virtual List<TEntity> GetAllPaging(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize, ref int totalRow)
        {
            var query = _repository.GetAll().Where(predicate);

            totalRow = query.Count();
            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).AsQueryable();

            return query.ToList();
        }

        public virtual TEntity GetById(TPrimaryKey id)
        {
            return _repository.GetById(id);
        }

        public virtual Task<List<TEntity>> GetListCondition(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.GetAllListAsync(predicate);
        }

        public virtual void Save()
        {
            _unitOfWork.Commit();
        }

        public virtual void Update(TEntity entity)
        {
            _repository.Update(entity);
        }
    }
}
