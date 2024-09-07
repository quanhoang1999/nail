using AutoMapper;
using Cms.Data.Entities.Content;
using Cms.Infrastructure.Dtos;
using Cms.Infrastructure.Interfaces;
using Cms.Service.Abstracts.Content;
using Cms.Service.Mapping;
using Cms.Service.ViewModel;
using Cms.Service.ViewModel.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cms.Service.Implement.Content
{
    public class ReviewService : WebServiceBase<Review, Guid, ReviewViewModel>, IReviewService
    {
        private IRepository<Review, Guid> _reviewRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ReviewService(IRepository<Review, Guid> reviewRepository,
            IUnitOfWork unitOfWork, IMapper mapper) : base(reviewRepository, unitOfWork, mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public void UpdateReview(ReviewViewModel viewModel)
        {
            var review = _reviewRepository.GetById(viewModel.Id);
            var dateCreated = review.DateCreated;
            review = viewModel.ToEntity(review);
            review.DateCreated = dateCreated;
            _reviewRepository.Update(review);
        }
        public PagedResult<ReviewViewModel> Filter(FilterCommonViewModel viewModel)
        {
            if (viewModel != null)
            {
                var filterModel = _reviewRepository.GetAll();
                //if (viewModel.KeyWord != null & viewModel.KeyWord.Length > 0)
                //    filterModel = filterModel.Where(c => c.Name.Contains(viewModel.KeyWord));
                if (!string.IsNullOrEmpty(viewModel.KeyWord))
                    filterModel = filterModel.Where(x => x.Name.Contains(viewModel.KeyWord));

                var messagesVm = filterModel.OrderByDescending(x => x.DateCreated).Skip((viewModel.PageIndex - 1) * viewModel.PageSize).Take(viewModel.PageSize).ToList();
                var totalRow = filterModel.Count();
                var messages = _mapper.Map<List<Review>, List<ReviewViewModel>>(messagesVm);
                var paginationSet = new PagedResult<ReviewViewModel>()
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
