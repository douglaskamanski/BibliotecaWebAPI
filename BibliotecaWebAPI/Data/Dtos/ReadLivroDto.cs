using BibliotecaWebAPI.Models;

namespace BibliotecaWebAPI.Data.Dtos;

public class ReadLivroDto
{
    public string Nome { get; set; }
    public int? AutorId { get; set; }
    public Autor? Autor { get; set; }
    public DateTime DataPublicacao { get; set; }
    public int? Edicao { get; set; }
    public string? Editora { get; set; }
    public string? Isbn { get; set; }
    public string Idioma { get; set; }
}
