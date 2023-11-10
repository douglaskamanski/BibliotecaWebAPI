namespace BibliotecaWebAPI.Data.Dtos;

public class ReadLivroNomeAutorDto
{
    public string Nome { get; set; }
    public int? AutorId { get; set; }
    public string? NomeAutor { get; set; }
    public DateTime DataPublicacao { get; set; }
    public int? Edicao { get; set; }
    public string? Editora { get; set; }
    public string? Isbn { get; set; }
    public string Idioma { get; set; }
}
