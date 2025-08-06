using Microsoft.AspNetCore.Http;
using SchoolSaas.Application.Common.Exceptions;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.Extensions;
using SchoolSaas.Infrastructure.Backoffice.OpenAPIs.EdulinkService;
using SchoolSaas.Infrastructure.Common.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolSaas.Infrastructure.Backoffice.ConnectedServices
{
    public class EdulinkConnectedService : IEdulinkConnectedService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;
        private readonly IDbLogger _logger;

        public EdulinkConnectedService(
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            HttpClient httpClient,
            IDbLogger logger)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ResponseDto<List<Domain.Common.DataObjects.Edulink.RegionDTO>>> GetAllRegions()
        {
            var apiResp = await ExecuteApiCallRef(
                client => client.GetAllRegionsAsync(),
                "Done");

            return MapList(apiResp, api => new Domain.Common.DataObjects.Edulink.RegionDTO
            {
                Id = api.Id,
                NameFr = api.NameFr,
                NameAr = api.NameAr,
                NameEn = api.NameEn
            });
        }

        public async Task<ResponseDto<Domain.Common.DataObjects.Edulink.RegionDTO>> GetRegionsById(Guid regionId)
        {
            var apiResp = await ExecuteApiCallRef(
                client => client.GetRegionByIdAsync(regionId),
                "Done");

            return MapItem(apiResp, api => new Domain.Common.DataObjects.Edulink.RegionDTO
            {
                Id = api.Id,
                NameFr = api.NameFr,
                NameAr = api.NameAr,
                NameEn = api.NameEn
            });
        }

        public async Task<ResponseDto<List<Domain.Common.DataObjects.Edulink.RegionDTO>>> SearchRegions(string nameFragment)
        {
            var apiResp = await ExecuteApiCallRef(
                client => client.SearchRegionsAsync(nameFragment),
                "Done");

            return MapList(apiResp, api => new Domain.Common.DataObjects.Edulink.RegionDTO
            {
                Id = api.Id,
                NameFr = api.NameFr,
                NameAr = api.NameAr,
                NameEn = api.NameEn
            });
        }

        public async Task<ResponseDto<List<Domain.Common.DataObjects.Edulink.CityDTO>>> GetAllCities()
        {
            var apiResp = await ExecuteApiCallRef(
                client => client.GetAllCitiesAsync(),
                "Done");

            return MapList(apiResp, api => new Domain.Common.DataObjects.Edulink.CityDTO
            {
                Id = api.Id,
                NameFr = api.NameFr,
                NameAr = api.NameAr,
                NameEn = api.NameEn,
                RegionId = api.RegionId
            });
        }

        public async Task<ResponseDto<Domain.Common.DataObjects.Edulink.CityDTO>> GetCityById(Guid cityId)
        {
            var apiResp = await ExecuteApiCallRef(
                client => client.GetCityByIdAsync(cityId),
                "Done");

            return MapItem(apiResp, api => new Domain.Common.DataObjects.Edulink.CityDTO
            {
                Id = api.Id,
                NameFr = api.NameFr,
                NameAr = api.NameAr,
                NameEn = api.NameEn,
                RegionId = api.RegionId
            });
        }

        public async Task<ResponseDto<List<Domain.Common.DataObjects.Edulink.CityDTO>>> SearchCities(string nameFragment)
        {
            var apiResp = await ExecuteApiCallRef(
                client => client.SearchCitiesAsync(nameFragment),
                "Done");

            return MapList(apiResp, api => new Domain.Common.DataObjects.Edulink.CityDTO
            {
                Id = api.Id,
                NameFr = api.NameFr,
                NameAr = api.NameAr,
                NameEn = api.NameEn,
                RegionId = api.RegionId
            });
        }

        public async Task<ResponseDto<List<Domain.Common.DataObjects.Edulink.CityDTO>>> GetCitiesByRegion(Guid regionId)
        {
            var apiResp = await ExecuteApiCallRef(
                client => client.GetCitiesByRegionAsync(regionId),
                "Done");

            return MapList(apiResp, api => new Domain.Common.DataObjects.Edulink.CityDTO
            {
                Id = api.Id,
                NameFr = api.NameFr,
                NameAr = api.NameAr,
                NameEn = api.NameEn,
                RegionId = api.RegionId
            });
        }

        public async Task<ResponseDto<List<Domain.Common.DataObjects.Edulink.SchoolMetadataDTO>>> GetAllSchools()
        {
            var apiResp = await ExecuteApiCallSchool(
                client => client.GetAllAsync(),
                "Done");

            return MapList(apiResp, api => new Domain.Common.DataObjects.Edulink.SchoolMetadataDTO
            {
                Id = api.Id,
                NameFr = api.NameFr,
                NameAr = api.NameAr,
                NameEn = api.NameEn,
                Code = api.Code,
                AddressFr = api.AddressFr,
                AddressAr = api.AddressAr,
                AddressEn = api.AddressEn,
                RegionId = api.RegionId,
                RegionNameFr = api.RegionNameFr,
                RegionNameAr = api.RegionNameAr,
                RegionNameEn = api.RegionNameEn,
                CityId = api.CityId,
                CityNameFr = api.CityNameFr,
                CityNameAr = api.CityNameAr,
                CityNameEn = api.CityNameEn,
                UseIsolatedDatabase = api.UseIsolatedDatabase,
                HasCustomConnectionString = api.HasCustomConnectionString,
                LogoUrl = api.LogoUrl,
                TimeZoneId = api.TimeZoneId
            });
        }

        public async Task<ResponseDto<Domain.Common.DataObjects.Edulink.SchoolMetadataDTO>> GetSchoolById(Guid schoolId)
        {
            var apiResp = await ExecuteApiCallSchool(
                client => client.GetByIdAsync(schoolId),
                "Done");

            return MapItem(apiResp, api => new Domain.Common.DataObjects.Edulink.SchoolMetadataDTO
            {
                Id = api.Id,
                NameFr = api.NameFr,
                NameAr = api.NameAr,
                NameEn = api.NameEn,
                Code = api.Code,
                AddressFr = api.AddressFr,
                AddressAr = api.AddressAr,
                AddressEn = api.AddressEn,
                RegionId = api.RegionId,
                RegionNameFr = api.RegionNameFr,
                RegionNameAr = api.RegionNameAr,
                RegionNameEn = api.RegionNameEn,
                CityId = api.CityId,
                CityNameFr = api.CityNameFr,
                CityNameAr = api.CityNameAr,
                CityNameEn = api.CityNameEn,
                UseIsolatedDatabase = api.UseIsolatedDatabase,
                HasCustomConnectionString = api.HasCustomConnectionString,
                LogoUrl = api.LogoUrl,
                TimeZoneId = api.TimeZoneId
            });
        }

        public async Task<ResponseDto<List<Domain.Common.DataObjects.Edulink.SchoolMetadataDTO>>> GetSchoolsByRegion(Guid regionId)
        {
            var apiResp = await ExecuteApiCallSchool(
                client => client.GetByRegionAsync(regionId),
                "Done");

            return MapList(apiResp, api => new Domain.Common.DataObjects.Edulink.SchoolMetadataDTO
            {
                Id = api.Id,
                NameFr = api.NameFr,
                NameAr = api.NameAr,
                NameEn = api.NameEn,
                Code = api.Code,
                AddressFr = api.AddressFr,
                AddressAr = api.AddressAr,
                AddressEn = api.AddressEn,
                RegionId = api.RegionId,
                RegionNameFr = api.RegionNameFr,
                RegionNameAr = api.RegionNameAr,
                RegionNameEn = api.RegionNameEn,
                CityId = api.CityId,
                CityNameFr = api.CityNameFr,
                CityNameAr = api.CityNameAr,
                CityNameEn = api.CityNameEn,
                UseIsolatedDatabase = api.UseIsolatedDatabase,
                HasCustomConnectionString = api.HasCustomConnectionString,
                LogoUrl = api.LogoUrl,
                TimeZoneId = api.TimeZoneId
            });
        }

        public async Task<ResponseDto<List<Domain.Common.DataObjects.Edulink.SchoolMetadataDTO>>> GetSchoolsByCity(Guid cityId)
        {
            var apiResp = await ExecuteApiCallSchool(
                client => client.GetByCityAsync(cityId),
                "Done");

            return MapList(apiResp, api => new Domain.Common.DataObjects.Edulink.SchoolMetadataDTO
            {
                Id = api.Id,
                NameFr = api.NameFr,
                NameAr = api.NameAr,
                NameEn = api.NameEn,
                Code = api.Code,
                AddressFr = api.AddressFr,
                AddressAr = api.AddressAr,
                AddressEn = api.AddressEn,
                RegionId = api.RegionId,
                RegionNameFr = api.RegionNameFr,
                RegionNameAr = api.RegionNameAr,
                RegionNameEn = api.RegionNameEn,
                CityId = api.CityId,
                CityNameFr = api.CityNameFr,
                CityNameAr = api.CityNameAr,
                CityNameEn = api.CityNameEn,
                UseIsolatedDatabase = api.UseIsolatedDatabase,
                HasCustomConnectionString = api.HasCustomConnectionString,
                LogoUrl = api.LogoUrl,
                TimeZoneId = api.TimeZoneId
            });
        }

        public async Task<ResponseDto<List<Domain.Common.DataObjects.Edulink.SchoolMetadataDTO>>> SearchSchool(string nameFragment)
        {
            var apiResp = await ExecuteApiCallSchool(
                client => client.SearchAsync(nameFragment),
                "Done");

            return MapList(apiResp, api => new Domain.Common.DataObjects.Edulink.SchoolMetadataDTO
            {
                Id = api.Id,
                NameFr = api.NameFr,
                NameAr = api.NameAr,
                NameEn = api.NameEn,
                Code = api.Code,
                AddressFr = api.AddressFr,
                AddressAr = api.AddressAr,
                AddressEn = api.AddressEn,
                RegionId = api.RegionId,
                RegionNameFr = api.RegionNameFr,
                RegionNameAr = api.RegionNameAr,
                RegionNameEn = api.RegionNameEn,
                CityId = api.CityId,
                CityNameFr = api.CityNameFr,
                CityNameAr = api.CityNameAr,
                CityNameEn = api.CityNameEn,
                UseIsolatedDatabase = api.UseIsolatedDatabase,
                HasCustomConnectionString = api.HasCustomConnectionString,
                LogoUrl = api.LogoUrl,
                TimeZoneId = api.TimeZoneId
            });
        }

        #region Helpers

        private async Task<ResponseDto<T>> ExecuteApiCallRef<T>(Func<ReferentialClient, Task<T>> apiCall, string successMessage = "")
        {
            try
            {
                var client = new ReferentialClient(_httpClient.BaseAddress!.AbsoluteUri, _httpClient, _httpContextAccessor);
                var result = await apiCall(client);

                return new ResponseDto<T> { Data = result, Message = string.IsNullOrWhiteSpace(successMessage) ? "Request successful" : successMessage };
            }
            catch (ApiException e) when (e.StatusCode == StatusCodes.Status400BadRequest)
            {
                throw new BusinessValidationException(getErreurMessage(e.Message));
            }
            catch (ApiException e) when (e.StatusCode == StatusCodes.Status404NotFound)
            {
                throw new NotFoundException(getErreurMessage(e.Message));
            }
            catch
            {
                throw;
            }
        }

        private async Task<ResponseDto<T>> ExecuteApiCallSchool<T>(Func<SchoolClient, Task<T>> apiCall, string successMessage = "")
        {
            try
            {
                var client = new SchoolClient(_httpClient.BaseAddress!.AbsoluteUri, _httpClient, _httpContextAccessor);
                var result = await apiCall(client);

                return new ResponseDto<T> { Data = result, Message = string.IsNullOrWhiteSpace(successMessage) ? "Request successful" : successMessage };
            }
            catch (ApiException e) when (e.StatusCode == StatusCodes.Status400BadRequest)
            {
                throw new BusinessValidationException(getErreurMessage(e.Message));
            }
            catch (ApiException e) when (e.StatusCode == StatusCodes.Status404NotFound)
            {
                throw new NotFoundException(getErreurMessage(e.Message));
            }
            catch
            {
                throw;
            }
        }

        private string getErreurMessage(string erreur)
        {
            return erreur.Split("detail")[1].RemoveChars(new[] { '\\', '}', '"' });
        }

        private ResponseDto<List<TDest>> MapList<TSource, TDest>(ResponseDto<ICollection<TSource>> source, Func<TSource, TDest> selector)
        {
            if (source.Data == null)
                return new ResponseDto<List<TDest>> { Data = null, Message = source.Message };

            var list = source.Data.Select(selector).ToList();
            return new ResponseDto<List<TDest>> { Data = list, Message = source.Message };
        }

        private ResponseDto<TDest> MapItem<TSource, TDest>(ResponseDto<TSource> source, Func<TSource, TDest> selector)
        {
            if (source.Data == null)
                return new ResponseDto<TDest> { Data = default, Message = source.Message };

            var dto = selector(source.Data);
            return new ResponseDto<TDest> { Data = dto, Message = source.Message };
        }

        #endregion
    }
}
