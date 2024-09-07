using AutoMapper;
using Cms.Business.ViewModel;
using Cms.Data.Entities.CRM;
using Cms.Data.Entities.Identity;
using Cms.Infrastructure.Dtos;
using Cms.Infrastructure.Interfaces;
using Cms.Service.Abstracts.Identiy;
using Cms.Service.ViewModel.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Cms.Service.Implement.Identity
{
    public class UserService : IUserService
    {

        private readonly IRepository<Customer, Guid> _customerRepository;

        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(
            IUnitOfWork unitOfWork,
            IRepository<Customer, Guid> customerRepository,

            RoleManager<AppRole> roleManager,
            UserManager<AppUser> userManager,
            IMapper mapper
            )
        {
            _userManager = userManager;
            _customerRepository = customerRepository;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> CreateAsync(RegisterViewModel viewModel)
        {
            try
            {
                var entityUser = _mapper.Map<RegisterViewModel, AppUser>(viewModel);
                entityUser.SelectedOaId = 1;
                var isSucceed = await _userManager.CreateAsync(entityUser, viewModel.Password);
                bool result = isSucceed.Succeeded;
                if (result)
                {
                    var roles = viewModel.Roles.ToArray();
                    await _userManager.AddToRolesAsync(entityUser, roles);
                    return true;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return false;
            }

            return false;
        }
        public async Task<bool> UpdateAsync(RegisterViewModel viewModel)
        {
            //var entityUser = _mapper.Map<RegisterViewModel, AppUser>(viewModel);
            var entityUser = await _userManager.FindByEmailAsync(viewModel.Email);
            if (entityUser != null)
            {
                entityUser.FullName = viewModel.FullName;
                entityUser.PhoneNumber = viewModel.PhoneNumber;
                entityUser.Gender = viewModel.Gender;
                entityUser.Address = viewModel.Address;
                entityUser.Avatar = viewModel.Avatar;
                //    entityUser.SelectedOaId = viewModel.SelectedOaId;
                var isSucceed = await _userManager.UpdateAsync(entityUser);
                bool result = isSucceed.Succeeded;
                if (result)
                {
                    if (viewModel.Roles != null)
                    {
                        var roles = viewModel.Roles.ToArray();
                        var currentRoles = await _userManager.GetRolesAsync(entityUser);
                        var succeed = await _userManager.AddToRolesAsync(entityUser, roles.Except(currentRoles).ToArray());
                        if (succeed.Succeeded)
                        {
                            string[] needRemoveRoles = currentRoles.Except(roles).ToArray();
                            await _userManager.RemoveFromRolesAsync(entityUser, needRemoveRoles);
                        }
                    }

                    return true;
                }

            }
            return false;
        }

        public PagedResult<RegisterViewModel> FilterUser(FilterUserViewModel viewModel, AppUser appUser)
        {
            IQueryable<AppUser> model = _userManager.Users;
            if (viewModel.SearchType == 1)
            {
                if (!string.IsNullOrEmpty(viewModel.Keyword))
                    model = model.Where(x => x.UserName.Contains(viewModel.Keyword));
            }
            else if (viewModel.SearchType == 2)
            {
                if (!string.IsNullOrEmpty(viewModel.Keyword))
                    model = model.Where(x => x.FullName.Contains(viewModel.Keyword));
            }
            else if (viewModel.SearchType == 3)
            {
                if (!string.IsNullOrEmpty(viewModel.Keyword))
                    model = model.Where(x => x.PhoneNumber.Contains(viewModel.Keyword));
            }
            if (viewModel.OaId > 0)
                model = model.Where(x => x.SelectedOaId == viewModel.OaId);

            if (viewModel.Status > 0)
            {
                //if (viewModel.Status == 1)
                //    model = model.Where(x => x.Status);
                //else
                //    model = model.Where(x => !x.Status);
            }

            int totalRow = model.Count();
            var filterModel = model.OrderByDescending(x => x.DateCreated).Skip((viewModel.PageIndex - 1) * viewModel.PageSize).Take(viewModel.PageSize).ToList();
            var appUserModel = _mapper.Map<List<AppUser>, List<RegisterViewModel>>(filterModel);
            var paginationSet = new PagedResult<RegisterViewModel>()
            {
                Results = appUserModel,
                CurrentPage = viewModel.PageIndex,
                RowCount = totalRow,
                PageSize = viewModel.PageSize
            };
            return paginationSet;
        }

        public async Task<bool> ChangePassAsync(AppUser appUser, ChangePassViewModel changePassViewModel)
        {
            if (_userManager.PasswordHasher.VerifyHashedPassword(appUser, appUser.PasswordHash, changePassViewModel.OldPass) == PasswordVerificationResult.Success)
            {
                appUser.PasswordHash = _userManager.PasswordHasher.HashPassword(appUser, changePassViewModel.NewPass);
                var result = await _userManager.UpdateAsync(appUser);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<RegisterViewModel> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            var userVm = _mapper.Map<AppUser, RegisterViewModel>(user);
            userVm.Roles = roles.ToList();
            return userVm;
        }

        public async Task DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
        }
    }
}
