using Cms.Data.Entities.Content;
using Cms.Infrastructure.Dtos;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Common;
using Cms.Service.ViewModel.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.Abstracts.Content
{
    public interface ISlideService : IWebServiceBase<Slide, int, SlideViewModel>
    {
        PagedResult<SlideViewModel> Filter(FilterCommonViewModel viewModel);
        void Copy(CopyViewModel viewModel);
    }
}
