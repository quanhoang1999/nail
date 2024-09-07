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
    public class FeedbackService : WebServiceBase<Feedback, Guid, FeedbackViewModel>, IFeedbackService
    {
        private IRepository<Feedback, Guid> _feedbackRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public FeedbackService(IRepository<Feedback, Guid> feedbackRepository,
            IUnitOfWork unitOfWork, IMapper mapper) : base(feedbackRepository, unitOfWork, mapper)
        {
            _feedbackRepository = feedbackRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public PagedResult<FeedbackViewModel> Filter(FilterCommonViewModel viewModel)
        {
            if (viewModel != null)
            {
                var filterModel = _feedbackRepository.GetAll();
                //if (viewModel.KeyWord != null & viewModel.KeyWord.Length > 0)
                //    filterModel = filterModel.Where(c => c.Name.Contains(viewModel.KeyWord));
                if (!string.IsNullOrEmpty(viewModel.KeyWord))
                    filterModel = filterModel.Where(x => x.Name.Contains(viewModel.KeyWord));

                var messagesVm = filterModel.OrderByDescending(x => x.DateCreated).Skip((viewModel.PageIndex - 1) * viewModel.PageSize).Take(viewModel.PageSize).ToList();
                var totalRow = filterModel.Count();
                var messages = _mapper.Map<List<Feedback>, List<FeedbackViewModel>>(messagesVm);
                var paginationSet = new PagedResult<FeedbackViewModel>()
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
    }
}
