using Cms.Data.Entities.Nails;
using Cms.Infrastructure.Dtos;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Nails;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.Abstracts.Nails
{
    public interface INailStoreService : IWebServiceBase<NailStore, int, NailStoreViewModel>
    {
        PagedResult<NailStoreViewModel> Filter(FilterCommonViewModel viewModel);
        string GetDescription(int id);
        void UpdateStore(NailStoreViewModel viewModel);
        IEnumerable<SelectListItem> GetStores();
    }
}
