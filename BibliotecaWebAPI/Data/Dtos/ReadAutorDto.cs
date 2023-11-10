namespace BibliotecaWebAPI.Data.Dtos;

public class ReadAutorDto
{
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string? Nacionalidade { get; set; }
    //public IList<Livro>? Livros { get; set; }
}
