

using AutoMapper;
using comerciales.Application.DTOs;
using comerciales.Domain.Entities;

namespace comerciales.Application.Profiles;

public class MappingMunicipioProfile : Profile
{
    public MappingMunicipioProfile()
    {
        CreateMap<Municipio, MunicipioDto>();
    }
}
