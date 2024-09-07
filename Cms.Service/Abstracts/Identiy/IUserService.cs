using Cms.Business.ViewModel;
using Cms.Data.Entities.Identity;
using Cms.Infrastructure.Dtos;
using Cms.Service.ViewModel.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Service.Abstracts.Identiy
{
    public interface IUserService
    {
        Task<bool> CreateAsync(RegisterViewModel viewModel);
        Task<bool> UpdateAsync(RegisterViewModel viewModel);
        Task DeleteAsync(string id);
        Task<RegisterViewModel> GetById(string id);
        PagedResult<RegisterViewModel> FilterUser(FilterUserViewModel viewModel, AppUser appUser);
        Task<bool> ChangePassAsync(AppUser appUser, ChangePassViewModel changePassViewModel);
    }
}
