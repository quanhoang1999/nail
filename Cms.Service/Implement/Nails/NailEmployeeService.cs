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
    public class NailEmployeeService : WebServiceBase<NailEmployee, Guid, NailEmployeeViewModel>, INailEmployeeService
    {
        private readonly IRepository<NailEmployee, Guid> _employeeRepository;
        private readonly IRepository<Cms.Data.Entities.Nails.NailEmployeeService, int> _serviceEmployeeRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public NailEmployeeService(IRepository<NailEmployee, Guid> employeeRepository,
            IRepository<Cms.Data.Entities.Nails.NailEmployeeService, int> serviceEmployeeRepository,
           IUnitOfWork unitOfWork, IMapper mapper) : base(employeeRepository, unitOfWork, mapper)
        {
            _employeeRepository = employeeRepository;
            _serviceEmployeeRepository = serviceEmployeeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool CreateUpdate(NailEmployeeViewModel viewModel)
        {
            if (viewModel.Id != Guid.Empty || viewModel.Id == null)
            {
                var employee = _employeeRepository.GetById(viewModel.Id);
                var dateCreated = employee.DateCreated;
                var avatar = employee.Avatar;
                employee = viewModel.ToEntity(employee);
                employee.DateCreated = dateCreated;
                employee.NailStore = null;
                if (string.IsNullOrEmpty(viewModel.Avatar) && !string.IsNullOrEmpty(avatar))
                    employee.Avatar = avatar;
                _employeeRepository.Update(employee);
                return true;
            }
            return false;
        }
        public bool UpdateStatus(NailEmployeeViewModel viewModel)
        {
            if (viewModel.Id != Guid.Empty || viewModel.Id == null)
            {
                var review = _employeeRepository.GetById(viewModel.Id);
                review.IsActive = viewModel.IsActive;
                _employeeRepository.Update(review);
                Save();
                return true;
            }
            return false;
        }
        public bool IsDelete(Guid id)
        {
            var model = _employeeRepository.GetById(id);
            model.IsDeleted = true;
            model.DateDeleted = DateTime.Now;
            _employeeRepository.Update(model);
            Save();
            return true;
        }

        public PagedResult<NailEmployeeViewModel> Filter(FilterCommonViewModel viewModel)
        {
            var query = _employeeRepository.GetAllIncluding(x => x.NailStore).Where(x => x.IsDeleted == false);
            if (!string.IsNullOrEmpty(viewModel.KeyWord))
                query = query.Where(x => x.Name.Contains(viewModel.KeyWord));

            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.DateCreated)
                .Skip((viewModel.PageIndex - 1) * viewModel.PageSize)
                .Take(viewModel.PageSize);
            var employees = _mapper.Map<List<NailEmployee>, List<NailEmployeeViewModel>>(data.ToList());
            var paginationSet = new PagedResult<NailEmployeeViewModel>()
            {
                Results = employees,
                CurrentPage = viewModel.PageIndex,
                RowCount = totalRow,
                PageSize = viewModel.PageSize,
            };

            return paginationSet;
        }

        public List<NailEmployeeViewModel> GetByServiceId(int serviceId)
        {
            var query = from p in _employeeRepository.GetAll()
                        join pt in _serviceEmployeeRepository.GetAll()
                        on p.Id equals pt.NailEmployeeId
                        where pt.NailServiceId == serviceId && p.IsActive
                        orderby p.DateCreated descending
                        select p;
            var model = _mapper.Map<List<NailEmployee>, List<NailEmployeeViewModel>>(query.ToList());
            return model;
        }
    }
}
