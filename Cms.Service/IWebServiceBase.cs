using Cms.Infrastructure.Dtos;
using Cms.Infrastructure.Enums;
using Cms.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Service
{
    public interface IWebServiceBase<TEntity, TPrimaryKey, ViewModel> where ViewModel : class
       where TEntity : DomainEntity<TPrimaryKey>
    {
        void Add(ViewModel viewModel);

        void Update(ViewModel viewModel);
        void UpdateV2(ViewModel viewModel, TPrimaryKey id);

        void Delete(TPrimaryKey id);

        ViewModel GetById(TPrimaryKey id);
        ViewModel GetByIdInclude(TPrimaryKey id, string[] includes = null);
        List<ViewModel> GetAll();
        Task<List<ViewModel>> GetAllAsync();
        PagedResult<ViewModel> GetAllPaging(Expression<Func<TEntity, bool>> predicate, Func<TEntity, bool> orderBy,
            SortDirection sortDirection, int pageIndex, int pageSize);

        void Save();
    }

}
