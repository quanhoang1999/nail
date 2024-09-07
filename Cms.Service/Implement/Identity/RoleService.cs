using AutoMapper;
using Cms.Data.Entities.Identity;
using Cms.Infrastructure.Dtos;
using Cms.Infrastructure.Interfaces;
using Cms.Service.Abstracts.Identiy;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Service.Implement
{
    public class RoleService : IRoleService
    {
        private RoleManager<AppRole> _roleManager;
        private IRepository<Function, string> _functionRepository;
        private IRepository<Permission, int> _permissionRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(RoleManager<AppRole> roleManager, IUnitOfWork unitOfWork,
         IRepository<Function, string> functionRepository, IRepository<Permission, int> permissionRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _functionRepository = functionRepository;
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }
        public async Task<bool> AddAsync(AppRoleViewModel roleVm)
        {
            var role = new AppRole()
            {
                Name = roleVm.Name,
                Description = roleVm.Description
            };
            var result = await _roleManager.CreateAsync(role);
            return result.Succeeded;
        }



        public Task<bool> CheckPermission(string functionId, string action, string[] roles)
        {
            var functions = _functionRepository.GetAll();
            var permissions = _permissionRepository.GetAll();
            var query = from f in functions
                        join p in permissions on f.Id equals p.FunctionId
                        join r in _roleManager.Roles on p.RoleId equals r.Id
                        where roles.Contains(r.Name) && f.Id == functionId
                        && ((p.CanCreate && action == "Create")
                        || (p.CanUpdate && action == "Update")
                        || (p.CanDelete && action == "Delete")
                        || (p.CanRead && action == "Read"))
                        select p;
            return query.AnyAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            await _roleManager.DeleteAsync(role);
        }

        public async Task<List<AppRoleViewModel>> GetAllAsync()
        {
            var model = await _roleManager.Roles.ToListAsync();
            var appRoles = _mapper.Map<List<AppRole>, List<AppRoleViewModel>>(model);
            return appRoles;
        }

        public PagedResult<AppRoleViewModel> GetAllPagingAsync(string keyword, int page, int pageSize)
        {
            var query = _roleManager.Roles;
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword)
                || x.Description.Contains(keyword));

            int totalRow = query.Count();
            query = query.Skip((page - 1) * pageSize)
               .Take(pageSize);

            var data = _mapper.Map<List<AppRole>, List<AppRoleViewModel>>(query.ToList());
            var paginationSet = new PagedResult<AppRoleViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public async Task<AppRoleViewModel> GetById(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            return _mapper.Map<AppRole, AppRoleViewModel>(role);
        }

        public List<PermissionViewModel> GetListFunctionWithRole(Guid roleId)
        {
            var functions = _functionRepository.GetAll();
            var permissions = _permissionRepository.GetAll();

            var query = from f in functions
                        join p in permissions on f.Id equals p.FunctionId into fp
                        from p in fp.DefaultIfEmpty()
                        where p != null && p.RoleId == roleId
                        select new PermissionViewModel()
                        {
                            RoleId = roleId,
                            FunctionId = f.Id,
                            CanCreate = p != null ? p.CanCreate : false,
                            CanDelete = p != null ? p.CanDelete : false,
                            CanRead = p != null ? p.CanRead : false,
                            CanUpdate = p != null ? p.CanUpdate : false
                        };
            return query.ToList();
        }

        public void SavePermission(List<PermissionViewModel> permissionVms, Guid roleId)
        {
            var permissions = _mapper.Map<List<PermissionViewModel>, List<Permission>>(permissionVms);
            var oldPermission = _permissionRepository.GetAll().Where(x => x.RoleId == roleId).ToList();
            if (oldPermission.Count > 0)
            {
                _permissionRepository.RemoveMultiple(oldPermission);
            }
            foreach (var permission in permissions)
            {
                _permissionRepository.Insert(permission);
            }
            _unitOfWork.Commit();
        }

        public async Task UpdateAsync(AppRoleViewModel roleVm)
        {
            var role = await _roleManager.FindByIdAsync(roleVm.Id.ToString());
            role.Description = roleVm.Description;
            role.Name = roleVm.Name;
            await _roleManager.UpdateAsync(role);
        }
    }
}
