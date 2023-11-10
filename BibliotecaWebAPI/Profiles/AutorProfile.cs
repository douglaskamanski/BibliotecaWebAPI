using AutoMapper;
using BibliotecaWebAPI.Data.Dtos;
using BibliotecaWebAPI.Models;

namespace BibliotecaWebAPI.Profiles;

public class AutorProfile : Profile
{
    public AutorProfile()
    {
        CreateMap<CreateAutorDto, Autor>();
        CreateMap<UpdateAutorDto, Autor>().ReverseMap();
        CreateMap<ReadAutorDto, Autor>().ReverseMap();
    }
}
