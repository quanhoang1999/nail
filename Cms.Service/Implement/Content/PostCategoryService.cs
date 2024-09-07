using AutoMapper;
using Cms.Data.Entities.Content;
using Cms.Infrastructure.Interfaces;
using Cms.Service.Abstracts.Content;
using Cms.Service.ViewModel.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.Implement.Content
{
    public class PostCategoryService:WebServiceBase<PostCategory, int, PostCategoryViewModel>, IPostCategoryService
    {
        private IRepository<PostCategory, int> _postCategoryRepository;
        private IRepository<PostTag, Guid> _postTagRepository;
        private IRepository<Tag, string> _tagRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PostCategoryService(IRepository<PostCategory, int> postCategoryRepository, IMapper mapper, IRepository<PostTag, Guid> postTagRepository,
            IRepository<Tag, string> tagRepository,
            IUnitOfWork unitOfWork) : base(postCategoryRepository, unitOfWork, mapper)
        {
            this._postCategoryRepository = postCategoryRepository;
            _postTagRepository = postTagRepository;
            _tagRepository = tagRepository;
            _mapper = mapper;
            this._unitOfWork = unitOfWork;
        }
        public void ReOrder(int sourceId, int targetId)
        {
            var source = _postCategoryRepository.GetById(sourceId);
            var target = _postCategoryRepository.GetById(targetId);
            int tempOrder = source.SortOrder;
            source.SortOrder = target.SortOrder;
            target.SortOrder = tempOrder;

            _postCategoryRepository.Update(source);
            _postCategoryRepository.Update(target);
        }

        public void UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items)
        {
            var sourceCategory = _postCategoryRepository.GetById(sourceId);
            sourceCategory.ParentId = targetId;
            _postCategoryRepository.Update(sourceCategory);

            //Get all sibling
            var sibling = _postCategoryRepository.GetAll(x => items.ContainsKey(x.Id));
            foreach (var child in sibling)
            {
                child.SortOrder = items[child.Id];
                _postCategoryRepository.Update(child);
            }
        }
    }
}
