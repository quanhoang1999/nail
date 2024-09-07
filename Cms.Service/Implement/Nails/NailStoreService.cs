using AutoMapper;
using Cms.Data.Entities.Nails;
using Cms.Infrastructure.Dtos;
using Cms.Infrastructure.Interfaces;
using Cms.Service.Abstracts.Nails;
using Cms.Service.Mapping;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Nails;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cms.Service.Implement.Nails
{
    public class NailStoreService : WebServiceBase<NailStore, int, NailStoreViewModel>, INailStoreService
    {
        private readonly IRepository<NailStore, int> _nailStoreRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public NailStoreService(IRepository<NailStore, int> nailStoreRepository,
           IUnitOfWork unitOfWork, IMapper mapper) : base(nailStoreRepository, unitOfWork, mapper)
        {
            _nailStoreRepository = nailStoreRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public PagedResult<NailStoreViewModel> Filter(FilterCommonViewModel viewModel)
        {
            var query = _nailStoreRepository.GetAll().Where(x => x.IsDeleted == false);
            if (!string.IsNullOrEmpty(viewModel.KeyWord))
                query = query.Where(x => x.Name.Contains(viewModel.KeyWord));
            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.DateCreated)
                .Skip((viewModel.PageIndex - 1) * viewModel.PageSize)
                .Take(viewModel.PageSize);
            var nailStores = _mapper.Map<List<NailStore>, List<NailStoreViewModel>>(data.ToList());
            var paginationSet = new PagedResult<NailStoreViewModel>()
            {
                Results = nailStores,
                CurrentPage = viewModel.PageIndex,
                RowCount = totalRow,
                PageSize = viewModel.PageSize,
            };
            return paginationSet;
        }
        public string GetDescription(int id)
        {
            string description = _nailStoreRepository.GetAll().Where(x => x.IsDeleted == false && x.Id==id).Select(s=>s.Description).FirstOrDefault();
            return description;
        }
        public void UpdateStore(NailStoreViewModel viewModel)
        {
            var entity = _nailStoreRepository.GetById(viewModel.Id);
            var dateCreated = entity.DateCreated;
            var imageEntity = entity.Image;
            entity = viewModel.ToEntity(entity);
            entity.DateCreated = dateCreated;
            if (string.IsNullOrEmpty(viewModel.Image) && !string.IsNullOrEmpty(imageEntity))
                entity.Image = imageEntity;
            _nailStoreRepository.Update(entity);
        }
        public IEnumerable<SelectListItem> GetStores()
        {
            List<SelectListItem> stores = _nailStoreRepository.GetAll(x => !x.IsDeleted).Select(n =>
                       new SelectListItem
                       {
                           Value = n.Id.ToString(),
                           Text = n.Name
                       }).ToList();
            var storetip = new SelectListItem()
            {
                Value = null,
                Text = "--- Select Store ---"
            };
            stores.Insert(0, storetip);
            return new SelectList(stores, "Value", "Text");
        }
    }
}
