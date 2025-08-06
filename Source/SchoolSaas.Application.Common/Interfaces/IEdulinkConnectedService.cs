using SchoolSaas.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IEdulinkConnectedService
    {
        Task<ResponseDto<List<Domain.Common.DataObjects.Edulink.RegionDTO>>> GetAllRegions();
        Task<ResponseDto<Domain.Common.DataObjects.Edulink.RegionDTO>> GetRegionsById(Guid regionId);
        Task<ResponseDto<List<Domain.Common.DataObjects.Edulink.RegionDTO>>> SearchRegions(string nameFragment);
        Task<ResponseDto<List<Domain.Common.DataObjects.Edulink.CityDTO>>> GetAllCities();
        Task<ResponseDto<Domain.Common.DataObjects.Edulink.CityDTO>> GetCityById(Guid cityId);
        Task<ResponseDto<List<Domain.Common.DataObjects.Edulink.CityDTO>>> SearchCities(string nameFragment);
        Task<ResponseDto<List<Domain.Common.DataObjects.Edulink.CityDTO>>> GetCitiesByRegion(Guid regionId);
        Task<ResponseDto<List<Domain.Common.DataObjects.Edulink.SchoolMetadataDTO>>> GetAllSchools();
        Task<ResponseDto<Domain.Common.DataObjects.Edulink.SchoolMetadataDTO>> GetSchoolById(Guid schoolId);
        Task<ResponseDto<List<Domain.Common.DataObjects.Edulink.SchoolMetadataDTO>>> GetSchoolsByRegion(Guid regionId);
        Task<ResponseDto<List<Domain.Common.DataObjects.Edulink.SchoolMetadataDTO>>> GetSchoolsByCity(Guid cityId);
        Task<ResponseDto<List<Domain.Common.DataObjects.Edulink.SchoolMetadataDTO>>> SearchSchool(string nameFragment);

    }
}
