using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.ViewModel.Customer
{
    public class FilterCustomerViewModel : FilterCommonViewModel
    {
        public int CustomerTypeId { get; set; } = -1;

        public string PhoneNumber { get; set; }
    }
}
