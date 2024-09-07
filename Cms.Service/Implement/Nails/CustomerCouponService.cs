using AutoMapper;
using Cms.Data.EF;
using Cms.Data.Entities.Nails;
using Cms.Infrastructure.Dtos;
using Cms.Infrastructure.Interfaces;
using Cms.Service.Abstracts.Nails;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Nails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cms.Service.Implement.Nails
{
    public class CustomerCouponService : WebServiceBase<CustomerCoupon, int, CustomerCouponViewModel>, ICustomerCouponService
    {
        private readonly IRepository<CustomerCoupon, int> _customerCouponRepository;

        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppDbContextDefault _context;
        public CustomerCouponService(IRepository<CustomerCoupon, int> customerCouponRepository,
           IUnitOfWork unitOfWork, IMapper mapper, AppDbContextDefault context) : base(customerCouponRepository, unitOfWork, mapper)
        {
            _customerCouponRepository = customerCouponRepository;
            _unitOfWork = unitOfWork;
            _context = context;
            _mapper = mapper;
        }
        public PagedResult<CustomerCouponViewModel> Filter(FilterCommonViewModel viewModel)
        {
            string[] includes = { "NailStore", "NailCustomer" };
            var query = _customerCouponRepository.GetAllIcludes(includes).Where(x => !x.IsDeleted);
            if (viewModel.SearchType == 1)
                query = query.Where(x => x.CouponCode.Contains(viewModel.KeyWord));
            else if (viewModel.SearchType == 2)
                query = query.Where(x => x.NailCustomer.Phone.Contains(viewModel.KeyWord));
            if (viewModel.StoreId > 0)
                query = query.Where(x => x.NailStoreId == viewModel.StoreId);
            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.DateCreated)
                .Skip((viewModel.PageIndex - 1) * viewModel.PageSize)
                .Take(viewModel.PageSize);
            var customerCoupons = _mapper.Map<List<CustomerCoupon>, List<CustomerCouponViewModel>>(data.ToList());
            var paginationSet = new PagedResult<CustomerCouponViewModel>()
            {
                Results = customerCoupons,
                CurrentPage = viewModel.PageIndex,
                RowCount = totalRow,
                PageSize = viewModel.PageSize,
            };
            return paginationSet;
        }
        public bool UpdateCoupon(CustomerCouponViewModel viewModel)
        {
            if (viewModel.Id > 0)
            {
                var coupon = _customerCouponRepository.GetById(viewModel.Id);
                if (viewModel.NailCustomerId > 0)
                    coupon.NailCustomerId = viewModel.NailCustomerId;
                if (!string.IsNullOrEmpty(viewModel.Description))
                    coupon.Description = viewModel.Description;
                if (viewModel.DateUsed.HasValue && !coupon.DateUsed.HasValue)
                    coupon.DateUsed = viewModel.DateUsed;
                coupon.DateStarted = viewModel.DateStarted;
                coupon.DateExpired = viewModel.DateExpired;
                if (viewModel.Discount > 0)
                    coupon.Discount = viewModel.Discount;
                if (viewModel.DiscountType > 0)
                    coupon.DiscountType = viewModel.DiscountType;
                if (!string.IsNullOrEmpty(viewModel.CouponCode))
                    coupon.CouponCode = viewModel.CouponCode;
                coupon.NailStoreId = viewModel.NailStoreId;
                _customerCouponRepository.Update(coupon);
                Save();
                return true;
            }
            return false;
        }
        public bool CreateCoupon(CustomerCouponViewModel viewModel)
        {
            if (viewModel.Id == 0)
            {
                var entity = _mapper.Map<CustomerCouponViewModel, CustomerCoupon>(viewModel);
                entity.CouponCode = _context.ExecuteFuncScalar("dbo.fn_GenerateCouponNo").ToString();
                entity.NailStore = null;
                _customerCouponRepository.Insert(entity);
                Save();
                return true;
            }
            return false;
        }
    }
}
