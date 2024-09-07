using Cms.Data.Entities.Identity;
using Cms.Infrastructure.Dtos;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Service.Abstracts.Identiy
{
    public interface IRoleService
    {
        Task<bool> CheckPermission(string functionId, string action, string[] roles);
        Task<bool> AddAsync(AppRoleViewModel userVm);

        Task DeleteAsync(Guid id);

        Task<List<AppRoleViewModel>> GetAllAsync();

        PagedResult<AppRoleViewModel> GetAllPagingAsync(string keyword, int page, int pageSize);

        Task<AppRoleViewModel> GetById(Guid id);


        Task UpdateAsync(AppRoleViewModel userVm);

        List<PermissionViewModel> GetListFunctionWithRole(Guid roleId);

        void SavePermission(List<PermissionViewModel> permissions, Guid roleId);
    }
}
