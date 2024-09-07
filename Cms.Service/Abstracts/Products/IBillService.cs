using Cms.Data.Entities.Products;
using Cms.Data.Enums;
using Cms.Infrastructure.Dtos;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.Abstracts.Products
{
    public interface IBillService:IWebServiceBase<Bill,int, BillViewModel>
    {
        List<ColorViewModel> GetColors();
        List<SizeViewModel> GetSizes();
        void UpdateStatus(int orderId, BillStatus status);
        PagedResult<BillViewModel> Filter(FilterCommonViewModel viewModel);
        BillViewModel GetByDetail(int id);
        ColorViewModel GetColor(int id);
        SizeViewModel GetSize(int id);
    }
}
