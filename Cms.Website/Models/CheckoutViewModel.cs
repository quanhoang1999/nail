using Cms.Data.Enums;
using Cms.Service.ViewModel.Common;
using Cms.Service.ViewModel.Products;
using Cms.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Website.Models
{
    public class CheckoutViewModel : BillViewModel
    {
        public List<ShoppingCartViewModel> Carts { get; set; }
        public List<EnumModel> PaymentMethods
        {
            get
            {
                return ((PaymentMethod[])Enum.GetValues(typeof(PaymentMethod)))
                    .Select(c => new EnumModel
                    {
                        Value = (int)c,
                        Name = c.GetDescription()
                    }).ToList();
            }
        }
    }
}
