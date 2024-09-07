using Cms.Data.Entities.Nails;
using Cms.Infrastructure.Dtos;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Nails;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.Abstracts.Nails
{
    public interface ICustomerCouponService : IWebServiceBase<CustomerCoupon, int, CustomerCouponViewModel>
    {
        PagedResult<CustomerCouponViewModel> Filter(FilterCommonViewModel viewModel);
        bool UpdateCoupon(CustomerCouponViewModel viewModel);
        bool CreateCoupon(CustomerCouponViewModel viewModel);
    }
}
