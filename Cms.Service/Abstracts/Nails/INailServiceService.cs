using Cms.Data.Entities.Nails;
using Cms.Infrastructure.Dtos;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Nails;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.Abstracts.Nails
{
    public interface INailServiceService : IWebServiceBase<NailService, int, NailServiceViewModel>
    {
        PagedResult<NailServiceViewModel> Filter(FilterCommonViewModel viewModel);
        void InsertService(NailServiceViewModel viewModel);
        void UpdateService(NailServiceViewModel viewModel);
        bool IsDelete(int id);
        bool DeleteFile(int id, string images);
        List<NailServiceViewModel> GetbyCategoryId(int id);
    }
}
