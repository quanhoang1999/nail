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
    public class ContactService : WebServiceBase<Contact, string, ContactViewModel>, IContactService
    {
        private IRepository<Contact, string> _contactRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ContactService(IRepository<Contact, string> contactRepository, IMapper mapper,
            IUnitOfWork unitOfWork) : base(contactRepository, unitOfWork, mapper)
        {
            this._contactRepository = contactRepository;
            _mapper = mapper;
            this._unitOfWork = unitOfWork;
        }
        //public PagedResult<ContactViewModel> Filter(FilterCommonViewModel viewModel)
        //{
        //    var filterModel = _contactRepository.GetAll();

        //    var model = filterModel.OrderByDescending(x => x.Name).Skip((viewModel.PageIndex - 1) * viewModel.PageSize).Take(viewModel.PageSize).ToList();
        //    var totalRow = filterModel.Count();
        //    var pageVm = _mapper.Map<List<Contact>, List<ContactViewModel>>(model);
        //    var paginationSet = new PagedResult<ContactViewModel>()
        //    {
        //        Results = pageVm,
        //        CurrentPage = viewModel.PageIndex,
        //        RowCount = totalRow,
        //        PageSize = viewModel.PageSize
        //    };
        //    return paginationSet;
        //}
    }
}
