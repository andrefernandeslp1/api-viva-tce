using API.Models;
using AutoMapper;

namespace API.DTOs.Mappings;

public class ApiDTOMappingProfile : Profile
{
    public ApiDTOMappingProfile()
    {
        CreateMap<Usuario, UsuarioDTO>().ReverseMap();
        CreateMap<Fornecedor, FornecedorDTO>().ReverseMap();  
        CreateMap<Servico, ServicoGetDTO>().ReverseMap();
        CreateMap<Servico, ServicoPostDTO>().ReverseMap();
        CreateMap<ServicoUsuario, ServicoUsuarioGetDTO>().ReverseMap();
        CreateMap<ServicoUsuario, ServicoUsuarioPostDTO>().ReverseMap();
    }
}
