using Cms.Data.Entities.Nails;
using Cms.Infrastructure.Dtos;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Nails;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.Abstracts.Nails
{
    public interface INailPromotionService : IWebServiceBase<NailPromotion, int, NailPromotionViewModel>
    {
        PagedResult<NailPromotionViewModel> Filter(FilterCommonViewModel viewModel);
        void UpdatePromotion(NailPromotionViewModel viewModel);
    }
}
