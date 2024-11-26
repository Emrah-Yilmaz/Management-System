using AutoMapper;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.WebApi.Areas.Locations.Models.Responses;

namespace ManagementSystem.WebApi.Areas.Locations.MappingProfile
{
    public class LocationMappingProfile : Profile
    {
        public LocationMappingProfile()
        {
            CreateMap<CityDto, CitiesResponse>();
            CreateMap<DistrictDto, DistrictsResponse>();
            CreateMap<QuarterDto, QuartersResponse>();
            CreateMap<AddressDto, AddressResponse>();
        }
    }
}
