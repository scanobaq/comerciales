
using AutoMapper;
using comerciales.Application.DTOs;
using comerciales.Domain.Entities;
using comerciales.Domain.Models;
namespace comerciales.Application.Profiles;

public class MappingComercianteProfile : Profile
{
    public MappingComercianteProfile()
    {
        CreateMap<Comerciante, ComercianteDto>().ReverseMap();
        CreateMap<FiltroParamsDto, FiltroParams>().ReverseMap();
    }
}
