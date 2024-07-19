using API.Models;
using AutoMapper;

namespace API.DTOs.Mappings;

public class ApiDTOMappingProfile : Profile
{
    public ApiDTOMappingProfile()
    {
        CreateMap<Usuario, UsuarioDTO>().ReverseMap();    
    }
}
