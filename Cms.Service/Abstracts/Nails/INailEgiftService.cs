using Cms.Data.Entities.Nails;
using Cms.Infrastructure.Dtos;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Nails;

namespace Cms.Service.Abstracts.Nails
{
    public interface INailEgiftService : IWebServiceBase<NailEGift, int, NailEgiftViewModel>
    {
        PagedResult<NailEgiftViewModel> Filter(FilterCommonViewModel viewModel);
    }
}
