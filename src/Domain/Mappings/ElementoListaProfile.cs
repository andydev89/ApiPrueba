using ApiPrueba.src.Api.Dtos;
using ApiPrueba.src.Application.Dtos;
using ApiPrueba.src.Domain.Entities;
using AutoMapper;

namespace ApiPrueba.src.Domain.Mappings;

public class ElementoListaProfile : Profile
{
    public ElementoListaProfile()
    {
       
        CreateMap<ElementoListaCreateDto, ElementoLista>();
       
        CreateMap<ElementoListaUpdateDto, ElementoLista>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
       
        CreateMap<ElementoLista, ElementoListaResponse>();
    }
}
