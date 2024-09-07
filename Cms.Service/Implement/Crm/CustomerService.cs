using AutoMapper;
using Cms.Data.Entities.CRM;
using Cms.Infrastructure.Dtos;
using Cms.Infrastructure.Interfaces;
using Cms.Service.Abstracts.Crm;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cms.Service.Implement.Crm
{
    public class CustomersService : ServiceBase<Customer, Guid>, ICustomersService
    {
        private readonly IRepository<Customer, Guid> _customerRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomersService(IRepository<Customer, Guid> customerRepository,
             IUnitOfWork unitOfWork, IMapper mapper) : base(customerRepository, unitOfWork)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }
        public PagedResult<CustomerViewModel> FilterUser(FilterCustomerViewModel viewModel)
        {
            var filterModel = _customerRepository.GetAll();
            if (!string.IsNullOrEmpty(viewModel.FullName))
                filterModel = filterModel.Where(c => c.CustomerName.Contains(viewModel.FullName));
            if (!string.IsNullOrEmpty(viewModel.PhoneNumber))
                filterModel = filterModel.Where(c => c.CustomerPhone.StartsWith(viewModel.PhoneNumber));
            if (viewModel.CustomerTypeId > -1)
                filterModel = filterModel.Where(c => c.CustomerTypeId == viewModel.CustomerTypeId);
            var customerVm = filterModel.OrderByDescending(x => x.DateCreated).Skip((viewModel.PageIndex - 1) * viewModel.PageSize).Take(viewModel.PageSize).ToList();
            var totalRow = filterModel.Count();
            var appUserModel = _mapper.Map<List<Customer>, List<CustomerViewModel>>(customerVm);
            var paginationSet = new PagedResult<CustomerViewModel>()
            {
                Results = appUserModel,
                CurrentPage = viewModel.PageIndex,
                RowCount = totalRow,
                PageSize = viewModel.PageSize
            };
            return paginationSet;
        }
        public bool UpdateCustomer(CustomerViewModel viewModel)
        {
            try
            {
                var customer = _mapper.Map<Customer>(viewModel);


                if (customer.Id == Guid.Empty || customer.Id == null)
                {
                    _customerRepository.Insert(customer);
                    // Save();
                }
                else
                {
                    var currentCustormer = _customerRepository.GetById(viewModel.Id);
                    if (currentCustormer != null)
                    {
                        currentCustormer.IsActive = customer.IsActive;
                        currentCustormer.CustomerName = customer.CustomerName;
                        currentCustormer.Address = customer.Address;
                        currentCustormer.CustomerPhone = customer.CustomerPhone;
                        currentCustormer.Gender = customer.Gender;
                        currentCustormer.Description = customer.Description;
                        currentCustormer.DistrictID = customer.DistrictID;
                        currentCustormer.WardID = customer.WardID;
                        currentCustormer.ProvinceID = customer.ProvinceID;
                        currentCustormer.FullAddress = customer.FullAddress;
                        _customerRepository.Update(currentCustormer);
                    }

                }
                Save();
                return true;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return false;
                throw;
            }
        }

        public Customer GtetById(Guid id)
        {
            return _customerRepository.GetById(id);
        }
    }
}
