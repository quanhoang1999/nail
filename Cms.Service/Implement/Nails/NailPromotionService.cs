using AutoMapper;
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

namespace Cms.Service.Implement.Nails
{
    public class NailPromotionService : WebServiceBase<NailPromotion, int, NailPromotionViewModel>, INailPromotionService
    {
        private readonly IRepository<NailPromotion, int> _nailPromotionRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public NailPromotionService(IRepository<NailPromotion, int> nailPromotionRepository,
           IUnitOfWork unitOfWork, IMapper mapper) : base(nailPromotionRepository, unitOfWork, mapper)
        {
            _nailPromotionRepository = nailPromotionRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public PagedResult<NailPromotionViewModel> Filter(FilterCommonViewModel viewModel)
        {
            var query = _nailPromotionRepository.GetAllIncluding(x => x.NailStore).Where(x => x.IsDeleted == false);
            if (viewModel.Status == 1)
                query = query.Where(x => x.IsActive == true);
            else if (viewModel.Status == 2)
                query = query.Where(x => x.IsActive == false);
            if (viewModel.StoreId > 0)
                query = query.Where(x => x.NailStoreId == viewModel.StoreId);
            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.DateCreated)
                .Skip((viewModel.PageIndex - 1) * viewModel.PageSize)
                .Take(viewModel.PageSize);
            var nailPromotions = _mapper.Map<List<NailPromotion>, List<NailPromotionViewModel>>(data.ToList());
            var paginationSet = new PagedResult<NailPromotionViewModel>()
            {
                Results = nailPromotions,
                CurrentPage = viewModel.PageIndex,
                RowCount = totalRow,
                PageSize = viewModel.PageSize,
            };
            return paginationSet;
        }

        public void UpdatePromotion(NailPromotionViewModel viewModel)
        {
            var review = _nailPromotionRepository.GetById(viewModel.Id);
            var dateCreated = review.DateCreated;
            review = viewModel.ToEntity(review);
            review.DateCreated = dateCreated;
            review.DateModified = DateTime.Now;
            review.NailStore = null;
            _nailPromotionRepository.Update(review);
        }
    }
}
