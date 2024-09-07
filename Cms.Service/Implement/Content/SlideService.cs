using AutoMapper;
using Cms.Data.Entities.Content;
using Cms.Infrastructure.Dtos;
using Cms.Infrastructure.Interfaces;
using Cms.Service.Abstracts.Common;
using Cms.Service.Abstracts.Content;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Common;
using Cms.Service.ViewModel.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cms.Service.Implement.Content
{
    public class SlideService : WebServiceBase<Slide, int, SlideViewModel>, ISlideService
    {
        private IRepository<Slide, int> _slideRepository;
        private IUploadService _uploadService;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SlideService(IRepository<Slide, int> slideRepository, IMapper mapper,
            IUnitOfWork unitOfWork,
            IUploadService uploadService) : base(slideRepository, unitOfWork, mapper)
        {
            this._slideRepository = slideRepository;
            _mapper = mapper;
            _uploadService = uploadService;
            this._unitOfWork = unitOfWork;
        }
        public PagedResult<SlideViewModel> Filter(FilterCommonViewModel viewModel)
        {
            var filterModel = _slideRepository.GetAll();

            var model = filterModel.OrderByDescending(x => x.SortOrder).Skip((viewModel.PageIndex - 1) * viewModel.PageSize).Take(viewModel.PageSize).ToList();
            var totalRow = filterModel.Count();
            var slideVm = _mapper.Map<List<Slide>, List<SlideViewModel>>(model);
            var paginationSet = new PagedResult<SlideViewModel>()
            {
                Results = slideVm,
                CurrentPage = viewModel.PageIndex,
                RowCount = totalRow,
                PageSize = viewModel.PageSize
            };
            return paginationSet;
        }
        public void Copy(CopyViewModel viewModel)
        {
            var entity = _slideRepository.GetById(viewModel.Id);
            entity.Id = 0;
            entity.Name = viewModel.Name;
            if (!viewModel.IsCopy)
                entity.Image = null;
            else
                entity.Image = _uploadService.CopyFile(entity.Image);
            _slideRepository.Insert(entity);
            Save();
        }
    }
}
