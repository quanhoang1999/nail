using Cms.Data.Entities.Nails;
using Cms.Infrastructure.Dtos;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Nails;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.Abstracts.Nails
{
    public interface INailEmployeeService : IWebServiceBase<NailEmployee, Guid, NailEmployeeViewModel>
    {
        bool CreateUpdate(NailEmployeeViewModel viewModel);
        PagedResult<NailEmployeeViewModel> Filter(FilterCommonViewModel viewModel);
        bool IsDelete(Guid id);
        bool UpdateStatus(NailEmployeeViewModel viewModel);
        List<NailEmployeeViewModel> GetByServiceId(int serviceId);
    }
}
