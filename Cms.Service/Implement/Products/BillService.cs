using AutoMapper;
using Cms.Data.Entities.Products;
using Cms.Data.Enums;
using Cms.Infrastructure.Dtos;
using Cms.Infrastructure.Interfaces;
using Cms.Service.Abstracts.Products;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cms.Service.Implement.Products
{
    public class BillService : WebServiceBase<Bill, int, BillViewModel>, IBillService
    {
        private readonly IRepository<Bill, int> _orderRepository;
        private readonly IRepository<BillDetail, int> _orderDetailRepository;
        private readonly IRepository<Color, int> _colorRepository;
        private readonly IRepository<Size, int> _sizeRepository;
        private readonly IRepository<Product, int> _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BillService(IRepository<Bill, int> orderRepository,
            IRepository<BillDetail, int> orderDetailRepository,
            IRepository<Color, int> colorRepository,
            IRepository<Product, int> productRepository,
            IRepository<Size, int> sizeRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(orderRepository, unitOfWork, mapper)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _colorRepository = colorRepository;
            _sizeRepository = sizeRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public PagedResult<BillViewModel> Filter(FilterCommonViewModel viewModel)
        {
            if (viewModel != null)
            {
                var filterModel = _orderRepository.GetAllIncluding(x => x.BillDetails);
                if (viewModel.KeyWord.Length > 0)
                    filterModel = filterModel.Where(c => c.CustomerName.Contains(viewModel.KeyWord));

                var messagesVm = filterModel.OrderByDescending(x => x.DateCreated).Skip((viewModel.PageIndex - 1) * viewModel.PageSize).Take(viewModel.PageSize).ToList();
                var totalRow = filterModel.Count();
                var messages = _mapper.Map<List<Bill>, List<BillViewModel>>(messagesVm);
                var paginationSet = new PagedResult<BillViewModel>()
                {
                    Results = messages,
                    CurrentPage = viewModel.PageIndex,
                    RowCount = totalRow,
                    PageSize = viewModel.PageSize
                };
                return paginationSet;
            }
            return null;
        }
        public List<ColorViewModel> GetColors()
        {
            var model = _colorRepository.GetAll().ToList();
            return _mapper.Map<List<Color>, List<ColorViewModel>>(model);
        }
        public BillViewModel GetByDetail(int id)
        {
            var model = _orderRepository.GetAllIncluding(x => x.BillDetails).FirstOrDefault(x => x.Id == id);
            return _mapper.Map<Bill, BillViewModel>(model);
        }
        public List<SizeViewModel> GetSizes()
        {
            var model = _sizeRepository.GetAll().ToList();
            return _mapper.Map<List<Size>, List<SizeViewModel>>(model);
        }
        public void UpdateStatus(int billId, BillStatus status)
        {
            var order = _orderRepository.GetById(billId);
            order.BillStatus = status;
            _orderRepository.Update(order);
        }
        public ColorViewModel GetColor(int id)
        {
            return _mapper.Map<Color, ColorViewModel>(_colorRepository.GetById(id));
        }

        public SizeViewModel GetSize(int id)
        {
            return _mapper.Map<Size, SizeViewModel>(_sizeRepository.GetById(id));
        }
    }
}
