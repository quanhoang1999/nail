using AutoMapper;
using Cms.Data.Entities.Content;
using Cms.Infrastructure.Dtos;
using Cms.Infrastructure.Interfaces;
using Cms.Service.Abstracts.Content;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cms.Service.Implement.Content
{
    public class PageService : WebServiceBase<Page, int, PageViewModel>, IPageService
    {
        private IRepository<Page, int> _pageRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PageService(IRepository<Page, int> pageRepository, IMapper mapper,
            IUnitOfWork unitOfWork) : base(pageRepository, unitOfWork, mapper)
        {
            this._pageRepository = pageRepository;
            _mapper = mapper;
            this._unitOfWork = unitOfWork;
        }

        public PagedResult<PageViewModel> Filter(FilterCommonViewModel viewModel)
        {
            var filterModel = _pageRepository.GetAll();

            var model = filterModel.OrderByDescending(x => x.Alias).Skip((viewModel.PageIndex - 1) * viewModel.PageSize).Take(viewModel.PageSize).ToList();
            var totalRow = filterModel.Count();
            var pageVm = _mapper.Map<List<Page>, List<PageViewModel>>(model);
            var paginationSet = new PagedResult<PageViewModel>()
            {
                Results = pageVm,
                CurrentPage = viewModel.PageIndex,
                RowCount = totalRow,
                PageSize = viewModel.PageSize
            };
            return paginationSet;
        }

        public PageViewModel GetByAlias(string alias)
        {
            var page = _pageRepository.FirstOrDefault(x => x.Alias == alias);
            return _mapper.Map<Page, PageViewModel>(page);
        }

    }
}
