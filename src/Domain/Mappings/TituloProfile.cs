using ApiPrueba.src.Api.Dtos;
using ApiPrueba.src.Application.Dtos;
using ApiPrueba.src.Domain.Entities;
using AutoMapper;

namespace ApiPrueba.src.Domain.Mappings;

public class TituloProfile : Profile
{
    public TituloProfile()
    {
       
        CreateMap<TituloCreateDto, Titulo>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

     
        CreateMap<TituloUpdateDto, Titulo>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

       
        CreateMap<Titulo, TituloResponse>();
    }
}