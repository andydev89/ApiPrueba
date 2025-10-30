using ApiPrueba.src.Api.Dtos;
using ApiPrueba.src.Application.Dtos;
using ApiPrueba.src.Domain.Entities;
using AutoMapper;

namespace ApiPrueba.src.Domain.Mappings;

public class ListaSeguimientoProfile : Profile
{
    public ListaSeguimientoProfile()
    {
        CreateMap<ListaSeguimientoCreateDto, ListaSeguimiento>();
        CreateMap<ListaSeguimientoUpdateDto, ListaSeguimiento>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<ListaSeguimiento, ListaSeguimientoResponse>();
    }
}

