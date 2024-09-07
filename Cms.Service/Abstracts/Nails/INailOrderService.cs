using Cms.Data.Entities.Nails;
using Cms.Infrastructure.Dtos;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Nails;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.Abstracts.Nails
{
    public interface INailOrderService : IWebServiceBase<NailOrder, int, NailOrderViewModel>
    {
        PagedResult<NailOrderViewModel> Filter(FilterCommonViewModel viewModel);
        void InsertOrder(NailOrderViewModel viewModel);
        void UpdateOrder(NailOrderViewModel viewModel);
        NailOrderViewModel GetByOrderId(int id);
        void UpdateStatus(int type, int[] ids);
    }
}
