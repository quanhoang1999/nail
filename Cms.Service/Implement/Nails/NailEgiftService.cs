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
    public class NailEgiftService : WebServiceBase<NailEGift, int, NailEgiftViewModel>, INailEgiftService
    {
        private readonly IRepository<NailEGift, int> _nailEgiftRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppDbContextDefault _context;

        public NailEgiftService(IRepository<NailEGift, int> nailEgiftRepository,
           
            AppDbContextDefault context,
           IUnitOfWork unitOfWork, IMapper mapper) : base(nailEgiftRepository, unitOfWork, mapper)
        {
            _nailEgiftRepository = nailEgiftRepository;
            _unitOfWork = unitOfWork;
            _context = context;
            _mapper = mapper;
        }

        public PagedResult<NailEgiftViewModel> Filter(FilterCommonViewModel viewModel)
        {
            var query = _nailEgiftRepository.GetAll().Where(x => x.IsDeleted == false);
            if (!string.IsNullOrEmpty(viewModel.KeyWord))
                query = query.Where(x => x.Name.Contains(viewModel.KeyWord));           
            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.DateCreated)
                .Skip((viewModel.PageIndex - 1) * viewModel.PageSize)
                .Take(viewModel.PageSize);
            var nailCustomers = _mapper.Map<List<NailEGift>, List<NailEgiftViewModel>>(data.ToList());
            var paginationSet = new PagedResult<NailEgiftViewModel>()
            {
                Results = nailCustomers,
                CurrentPage = viewModel.PageIndex,
                RowCount = totalRow,
                PageSize = viewModel.PageSize,
            };
            return paginationSet;
        }
    }
}
