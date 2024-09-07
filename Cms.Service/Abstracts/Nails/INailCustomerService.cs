using Cms.Data.Entities.Nails;
using Cms.Infrastructure.Dtos;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Nails;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Service.Abstracts.Nails
{
    public interface INailCustomerService : IWebServiceBase<NailCustomer, int, NailCustomerViewModel>
    {
        PagedResult<NailCustomerViewModel> Filter(FilterCommonViewModel viewModel);
        void UpdateNailCustomer(NailCustomerViewModel viewModel);
        Task<bool> SignInAsync(NailCustomerViewModel viewModel);
        bool IsDelete(int id);
    }
}
