using Cms.Data.Entities.Country;
using Cms.Service.ViewModel.Country;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.Abstracts.Countries
{
    public interface ICountryService : IServiceBase<Country, int>
    {
        List<ProvinceViewModel> GetProvinceViewModels(int countryId = 237);
        List<DistrictViewModel> GetDistrictViewModels(int province);
        List<WardViewModel> GetWardViewModels(int district);
    }
}
