using ApiPrueba.src.Api.Dtos;
using ApiPrueba.src.Application.Dtos;
using ApiPrueba.src.Domain.Entities;
using AutoMapper;

namespace ApiPrueba.src.Domain.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UsuarioDto, User>();
        CreateMap<UsuarioUpdateDto, User>();
        CreateMap<User, UsuarioUpdateDto>();
        CreateMap<User, UsuarioDto>();
        CreateMap<UserResponse, User>();
        CreateMap<User, UserResponse>();

    }
}