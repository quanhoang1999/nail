using AutoMapper;
using Cms.Data.Entities.Nails;
using Cms.Infrastructure.Dtos;
using Cms.Infrastructure.Interfaces;
using Cms.Service.Abstracts.Nails;
using Cms.Service.Mapping;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Nails;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cms.Service.Implement.Nails
{
    public class NailOrderService : WebServiceBase<NailOrder, int, NailOrderViewModel>, INailOrderService
    {
        private readonly IRepository<NailOrder, int> _nailOrderRepository;
        private readonly IRepository<NailOrderDetail, int> _nailOrderDetailRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public NailOrderService(IRepository<NailOrder, int> nailOrderRepository,
            IRepository<NailOrderDetail, int> nailOrderDetailRepository,
           IUnitOfWork unitOfWork, IMapper mapper) : base(nailOrderRepository, unitOfWork, mapper)
        {
            _nailOrderRepository = nailOrderRepository;
            _nailOrderDetailRepository = nailOrderDetailRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public PagedResult<NailOrderViewModel> Filter(FilterCommonViewModel viewModel)
        {
            var query = _nailOrderRepository.GetAllIncluding(x => x.NailStore).Where(x => x.IsDeleted == false);
            if (viewModel.SearchType == 2)
                query = query.Where(x => x.Email.Contains(viewModel.KeyWord));
            else if (viewModel.SearchType == 1)
                query = query.Where(x => x.PhoneNumber.Contains(viewModel.KeyWord));
            else if (viewModel.SearchType == 3)
                query = query.Where(x => x.CustomerName.Contains(viewModel.KeyWord));
            if (viewModel.StoreId > 0)
                query = query.Where(x => x.NailStoreId == viewModel.StoreId);
            var toDate = viewModel.ToDate?.AddDays(1);
            if (viewModel.ToDate != null && viewModel.FromDate != null)
                query = query.Where(x => x.DatePick >= viewModel.FromDate && x.DatePick <= toDate);
            else if (viewModel.ToDate != null)
                query = query.Where(x => x.DatePick <= toDate);
            else if (viewModel.FromDate != null)
                query = query.Where(x => x.DatePick >= viewModel.FromDate);
            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.DateCreated)
                .Skip((viewModel.PageIndex - 1) * viewModel.PageSize)
                .Take(viewModel.PageSize);
            var nailOrders = _mapper.Map<List<NailOrder>, List<NailOrderViewModel>>(data.ToList());
            var paginationSet = new PagedResult<NailOrderViewModel>()
            {
                Results = nailOrders,
                CurrentPage = viewModel.PageIndex,
                RowCount = totalRow,
                PageSize = viewModel.PageSize,
            };
            return paginationSet;
        }
        public NailOrderViewModel GetByOrderId(int id)
        {
            var query = _nailOrderRepository.GetAll().Include(c => c.NailOrderDetails).ThenInclude(t => t.NailEmployee).Where(x => x.Id == id).FirstOrDefault();
            var nailOrder = _mapper.Map<NailOrder, NailOrderViewModel>(query);
            return nailOrder;
        }
        public void UpdateStatus(int type, int[] ids)
        {
            if (type == 3)
            {
                var model = _nailOrderRepository.GetAll(x => ids.Contains(x.Id)).ToList();
                model.ForEach(a => a.IsDeleted = true);
            }
            else
            {
                bool isUpdate = type == 1 ? true : false;
                var model = _nailOrderRepository.GetAll(x => ids.Contains(x.Id)).ToList();
                model.ForEach(a => a.IsFinish = isUpdate);
            }

            Save();
        }

        public void InsertOrder(NailOrderViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(NailOrderViewModel viewModel)
        {
            var nailService = _nailOrderRepository.GetById(viewModel.Id);
            var dateCreated = nailService.DateCreated;
            nailService = viewModel.ToEntity(nailService);
            nailService.DateCreated = dateCreated;
            nailService.NailStore = null;
            _nailOrderRepository.Update(nailService);
            Save();
        }
    }
}
