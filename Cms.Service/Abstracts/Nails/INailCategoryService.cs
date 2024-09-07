using Cms.Data.Entities.Nails;
using Cms.Infrastructure.Dtos;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Nails;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.Abstracts.Nails
{
    public interface INailCategoryService : IWebServiceBase<NailCategory, int, NailCategoryViewModel>
    {
        PagedResult<NailCategoryViewModel> Filter(FilterCommonViewModel viewModel);
        List<NailCategoryViewModel> GetListShowOnHomePage(int storeId);
        List<NailCategoryViewModel> GetListShowOnMenu(int storeId);
        List<NailCategoryViewModel> GetListShowOnMenu();
        List<NailCategoryViewModel> GetAllInclude(int storeId);
        List<NailCategoryViewModel> GetAllInclude();
        void InsertCategory(NailCategoryViewModel viewModel);
        void UpdateCategory(NailCategoryViewModel viewModel);
        bool IsDelete(int id);
        bool DeleteFile(int id, string images);
    }
}
