﻿using AutoMapper;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.WebApi.Areas.Histories.Models.Responses;
using System.Text.Json;

namespace ManagementSystem.WebApi.Areas.Histories.MappingProfile
{
    public class HistoryMappingProfile : Profile
    {
        public HistoryMappingProfile()
        {
            CreateMap<HistoryDto, LogResponse>()
                       .ForMember(dest => dest.ChangedBy, opt =>
                            opt.MapFrom(src => JsonSerializer.Deserialize<ChangedByInfo>(src.ChangedBy, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })));

            CreateMap<List<HistoryDto>, HistoryResponse>()
                .ConvertUsing<HistoryDtoListConverter>();
        }
    }
    public class HistoryDtoListConverter : ITypeConverter<List<HistoryDto>, HistoryResponse>
    {
        public HistoryResponse Convert(List<HistoryDto> source, HistoryResponse destination, ResolutionContext context)
        {
            return new HistoryResponse
            {
                Entity = source.FirstOrDefault()?.TableName,
                Logs = context.Mapper.Map<List<LogResponse>>(source)
            };
        }
    }
}
