using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaWebAPI.Models;

[Table("Livros")]
public class Livro
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "{0} é obrigatório")]
    [StringLength(300, MinimumLength = 1, ErrorMessage = "{0} deve ter o tamanho entre {2} e {1} caracteres")]
    public string Nome { get; set; }

    public int? AutorId { get; set; }
    public Autor? Autor { get; set; }

    [Display(Name = "Data de publicação")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
    [Required(ErrorMessage = "{0} é obrigatório")]
    public DateTime DataPublicacao { get; set; }

    [Display(Name = "Edição")]
    [Range(1, 100, ErrorMessage = "{0} deve estar entre {1} e {2}")]
    public int? Edicao { get; set; }

    [StringLength(100, ErrorMessage = "{0} deve ter o tamanho máximo de {1} caracteres")]
    public string? Editora { get; set; }

    [Display(Name = "ISBN")]
    [StringLength(30, ErrorMessage = "{0} deve ter o tamanho máximo de {1} caracteres")]
    public string? Isbn { get; set; }

    [Required(ErrorMessage = "{0} é obrigatório")]
    [StringLength(30, MinimumLength = 1, ErrorMessage = "{0} deve ter o tamanho entre {2} e {1} caracteres")]
    public string Idioma { get; set; }
}
