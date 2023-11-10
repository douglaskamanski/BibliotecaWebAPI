using AutoMapper;
using BibliotecaWebAPI.Data.Dtos;
using BibliotecaWebAPI.Models;

namespace BibliotecaWebAPI.Profiles;

public class LivroProfile : Profile
{
    public LivroProfile()
    {
        CreateMap<CreateLivroDto, Livro>();
        CreateMap<UpdateLivroDto, Livro>().ReverseMap();
        CreateMap<ReadLivroDto, Livro>().ReverseMap();
        CreateMap<Livro, ReadLivroNomeAutorDto>().ForMember(p => p.NomeAutor, map => map.MapFrom(src => src.Autor.Nome));
    }
}
