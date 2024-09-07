using AutoMapper;
using Cms.Data.Interfaces;
using Cms.Infrastructure.Dtos;
using Cms.Infrastructure.Enums;
using Cms.Infrastructure.Interfaces;
using Cms.Infrastructure.SharedKernel;
using Cms.Service.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cms.Service
{
    public class WebServiceBase<TEntity, TPrimaryKey, ViewModel> : IWebServiceBase<TEntity, TPrimaryKey, ViewModel>
        where ViewModel : class
        where TEntity : DomainEntity<TPrimaryKey>
    {
        private readonly IRepository<TEntity, TPrimaryKey> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        protected WebServiceBase(IRepository<TEntity, TPrimaryKey> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public virtual void Add(ViewModel viewModel)
        {
            var model = _mapper.Map<ViewModel, TEntity>(viewModel);
            _repository.Insert(model);
        }

        public virtual void Delete(TPrimaryKey id)
        {
            _repository.Delete(id);
        }

        public virtual ViewModel GetById(TPrimaryKey id)
        {
            return _mapper.Map<TEntity, ViewModel>(_repository.GetById(id));
        }
        public virtual ViewModel GetByIdInclude(TPrimaryKey id, string[] includes = null)
        {
            return _mapper.Map<TEntity, ViewModel>(_repository.GetByIdInclude(id, includes));
        }
        public virtual List<ViewModel> GetAll()
        {
            var query = _repository.GetAll(false).ToList();
            var data = _mapper.Map<List<TEntity>, List<ViewModel>>(query);
            return data;
        }
        public virtual Task<List<ViewModel>> GetAllAsync()
        {
            var query = _repository.GetAll();
            var data = _mapper.Map<IQueryable<TEntity>, IQueryable<ViewModel>>(query);
            return data.ToListAsync();
        }

        public virtual PagedResult<ViewModel> GetAllPaging(Expression<Func<TEntity, bool>> predicate, Func<TEntity, bool> orderBy,
            SortDirection sortDirection, int pageIndex, int pageSize)
        {
            var query = _repository.GetAll().Where(predicate);

            int totalRow = query.Count();

            if (sortDirection == SortDirection.Ascending)
            {
                query = query.OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsQueryable();
            }
            else
            {
                query = query.OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsQueryable();
            }

            var data = _mapper.Map<IQueryable<TEntity>, IQueryable<ViewModel>>(query);
            var paginationSet = new PagedResult<ViewModel>()
            {
                Results = data.ToList(),
                CurrentPage = pageIndex,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public virtual void Save()
        {
            _unitOfWork.Commit();
        }

        public virtual void Update(ViewModel viewModel)
        {
            var model = _mapper.Map<ViewModel, TEntity>(viewModel);
            _repository.Update(model);
        }
        public virtual void UpdateV2(ViewModel viewModel, TPrimaryKey id)
        {
            var entity = _repository.GetById(id);
            entity = viewModel.ToEntity(entity);
            _repository.Update(entity);
        }
       

    }

}
