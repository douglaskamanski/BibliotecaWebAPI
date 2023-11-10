using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaWebAPI.Models;

[Table("Autores")]
public class Autor
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "{0} é obrigatório")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} deve ter o tamanho entre {2} e {1} caracteres")]
    public string Nome { get; set; }

    [Display(Name = "Data de nascimento")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
    [Required(ErrorMessage = "{0} é obrigatório")]
    public DateTime DataNascimento { get; set; }

    [StringLength(30, ErrorMessage = "{0} deve ter o tamanho máximo de {1} caracteres")]
    public string? Nacionalidade { get; set; }

    public IList<Livro>? Livros { get; set; }
}
