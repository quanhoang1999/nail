using Cms.Data.Entities.CRM;
using Cms.Infrastructure.Dtos;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.Abstracts.Crm
{
    public interface ICustomersService : IServiceBase<Customer, Guid>
    {
        PagedResult<CustomerViewModel> FilterUser(FilterCustomerViewModel viewModel);
        Customer GtetById(Guid id);
        bool UpdateCustomer(CustomerViewModel viewModel);
    }
}
