using AutoMapper;
using Cms.Data.Entities.Country;
using Cms.Infrastructure.Interfaces;
using Cms.Service.Abstracts.Countries;
using Cms.Service.ViewModel.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cms.Service.Implement.Countries
{
    public class CountryService : ServiceBase<Country, int>, ICountryService
    {
        private readonly IRepository<Country, int> _countryRepository;
        private readonly IRepository<Province, int> _provinceRepository;
        private readonly IRepository<District, int> _districtRepository;
        private readonly IRepository<Ward, int> _wardRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountryService(IRepository<Country, int> countryRepository, IRepository<Province, int> provinceRepository,
            IRepository<District, int> districtRepository, IRepository<Ward, int> wardRepository,
             IUnitOfWork unitOfWork, IMapper mapper) : base(countryRepository, unitOfWork)
        {
            _countryRepository = countryRepository;
            _provinceRepository = provinceRepository;
            _wardRepository = wardRepository;
            _districtRepository = districtRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }

        public List<ProvinceViewModel> GetProvinceViewModels(int countryId = 237)
        {
            var provinces = _provinceRepository.GetAll(x => x.CountryId == countryId).ToList();
            var provinceVm = _mapper.Map<List<Province>, List<ProvinceViewModel>>(provinces);
            return provinceVm;
        }

        public List<DistrictViewModel> GetDistrictViewModels(int province)
        {
            var distrincts = _districtRepository.GetAll(x => x.ProvinceId == province).ToList();
            var distrinctVm = _mapper.Map<List<District>, List<DistrictViewModel>>(distrincts);
            return distrinctVm;
        }

        public List<WardViewModel> GetWardViewModels(int district)
        {
            var wards = _wardRepository.GetAll(x => x.DistrictID == district).ToList();
            var wardVm = _mapper.Map<List<Ward>, List<WardViewModel>>(wards);
            return wardVm;
        }
    }
}
