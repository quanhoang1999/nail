using AutoMapper;
using Cms.Data.Entities.Identity;
using Cms.Infrastructure.Interfaces;
using Cms.Service.Abstracts.Identiy;
using Cms.Service.ViewModel.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cms.Service.Implement
{
    public class FunctionService : WebServiceBase<Function, string, FunctionViewModel>, IFunctionService
    {
        private readonly IRepository<Function, string> _functionRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public FunctionService(IRepository<Function, string> functionRepository, IMapper mapper,
           IUnitOfWork unitOfWork) : base(functionRepository, unitOfWork, mapper)
        {
            _functionRepository = functionRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public bool CheckExistedId(string id)
        {
            return _functionRepository.GetById(id) != null;
        }




        public IEnumerable<FunctionViewModel> GetAllWithParentId(string parentId)
        {
            var model = _functionRepository.GetAllList(x => x.ParentId == parentId);
            var fuctionVm = _mapper.Map<List<Function>, List<FunctionViewModel>>(model);
            return fuctionVm;
        }

        public void ReOrder(string sourceId, string targetId)
        {
            var source = _functionRepository.GetById(sourceId);
            var target = _functionRepository.GetById(targetId);
            int tempOrder = source.SortOrder;

            source.SortOrder = target.SortOrder;
            target.SortOrder = tempOrder;

            _functionRepository.Update(source);
            _functionRepository.Update(target);
        }

        public void UpdateParentId(string sourceId, string targetId, Dictionary<string, int> items)
        {
            //Update parent id for source
            var category = _functionRepository.GetById(sourceId);
            category.ParentId = targetId;
            _functionRepository.Update(category);

            //Get all sibling
            var sibling = _functionRepository.GetAllList(x => items.ContainsKey(x.Id));
            foreach (var child in sibling)
            {
                child.SortOrder = items[child.Id];
                _functionRepository.Update(child);
            }
        }
    }
}
