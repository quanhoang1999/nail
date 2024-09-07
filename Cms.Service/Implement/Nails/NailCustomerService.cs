using AutoMapper;
using Cms.Data.EF;
using Cms.Data.Entities.Nails;
using Cms.Infrastructure.Dtos;
using Cms.Infrastructure.Interfaces;
using Cms.Service.Abstracts.Nails;
using Cms.Service.Mapping;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Nails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Service.Implement.Nails
{
    public class NailCustomerService : WebServiceBase<NailCustomer, int, NailCustomerViewModel>, INailCustomerService
    {
        private readonly IRepository<NailCustomer, int> _nailCustomerRepository;
        private readonly IRepository<CustomerCoupon, int> _customerRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppDbContextDefault _context;
        public NailCustomerService(IRepository<NailCustomer, int> nailCustomerRepository,
            AppDbContextDefault context, IRepository<CustomerCoupon, int> customerRepository,
           IUnitOfWork unitOfWork, IMapper mapper) : base(nailCustomerRepository, unitOfWork, mapper)
        {
            _nailCustomerRepository = nailCustomerRepository;
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _context = context;
            _mapper = mapper;
        }
        public PagedResult<NailCustomerViewModel> Filter(FilterCommonViewModel viewModel)
        {
            var query = _nailCustomerRepository.GetAllIncluding(x => x.NailStore).Where(x => x.IsDeleted == false);
            if (viewModel.SearchType == 1)
                query = query.Where(x => x.Email.Contains(viewModel.KeyWord));
            else if (viewModel.SearchType == 2)
                query = query.Where(x => x.Phone.Contains(viewModel.KeyWord));
            else if (viewModel.SearchType == 3)
                query = query.Where(x => x.FirstName.Contains(viewModel.KeyWord) || x.LastName.Contains(viewModel.KeyWord));
            if (viewModel.StoreId > 0)
                query = query.Where(x => x.NailStoreId == viewModel.StoreId);
            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.DateCreated)
                .Skip((viewModel.PageIndex - 1) * viewModel.PageSize)
                .Take(viewModel.PageSize);
            var nailCustomers = _mapper.Map<List<NailCustomer>, List<NailCustomerViewModel>>(data.ToList());
            var paginationSet = new PagedResult<NailCustomerViewModel>()
            {
                Results = nailCustomers,
                CurrentPage = viewModel.PageIndex,
                RowCount = totalRow,
                PageSize = viewModel.PageSize,
            };
            return paginationSet;
        }
        public void UpdateNailCustomer(NailCustomerViewModel viewModel)
        {
            var review = _nailCustomerRepository.GetById(viewModel.Id);
            var dateCreated = review.DateCreated;
            review = viewModel.ToEntity(review);
            review.DateCreated = dateCreated;
            review.NailStore = null;
            _nailCustomerRepository.Update(review);
        }

        public async Task<bool> SignInAsync(NailCustomerViewModel viewModel)
        {
            var countNail = await _nailCustomerRepository.CountAsync(x => x.Phone == viewModel.Phone);
            if (countNail <= 0)
            {
                var entity = _mapper.Map<NailCustomerViewModel, NailCustomer>(viewModel);

                var nailCustomer = _nailCustomerRepository.Insert(entity);
                Save();
                var coupon = new CustomerCoupon();
                coupon.CouponCode = _context.ExecuteFuncScalar("dbo.fn_GenerateCouponNo").ToString();
                coupon.NailCustomerId = nailCustomer.Id;
                coupon.DiscountType = 1;
                coupon.Discount = 10;
                _customerRepository.Insert(coupon);
                Save();
                return true;
            }
            return false;

        }
        public bool IsDelete(int id)
        {
            var model = _nailCustomerRepository.GetById(id);
            model.IsDeleted = true;
            model.DateDeleted = DateTime.Now;
            _nailCustomerRepository.Update(model);
            Save();
            return true;
        }
    }
}
