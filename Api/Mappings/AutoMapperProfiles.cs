using Api.Models.Domains;
using Api.Models.DTOs;
using AutoMapper;

namespace Api.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Region, RegionDTO>().ReverseMap();
        CreateMap<AddRegionRequestDto, Region>().ReverseMap();
        CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
        CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
        CreateMap<Walk, WalkDTO>().ReverseMap();
        CreateMap<Difficulty, DifficultyDTO>().ReverseMap();
        CreateMap<Walk, UpdateWalkRequestDto>().ReverseMap();
    }
}