using AutoMapper;
using BibliotecaWebAPI.Data;
using BibliotecaWebAPI.Data.Dtos;
using BibliotecaWebAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LivroController : ControllerBase
{
    private BibliotecaContext _context;
    private IMapper _mapper;

    public LivroController(BibliotecaContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Busca todos livros cadastrados
    /// </summary>
    /// <param name="take">Recupera x registros por consulta</param>
    /// <param name="skip">Pula x registros. Utilizado para paginação</param>
    /// <returns>IEnumerable ReadLivroDto</returns>
    /// <response code="200">Caso a consulta seja feita com sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadLivroDto> BuscaTodosLivros([FromQuery] int take = 20, [FromQuery] int skip = 0)
    {
        return _mapper.Map<List<ReadLivroDto>>(_context.Livros.Skip(skip).Take(take));
    }

    /// <summary>
    /// Busca livro por Id
    /// </summary>
    /// <param name="id">Id do livro</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a consulta seja feita com sucesso</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult BuscaLivroPorId(int id)
    {
        var livro = _context.Livros
                            .Include(l => l.Autor)
                            .FirstOrDefault(livro => livro.Id == id);

        if (livro == null) return NotFound();

        var livroDto = _mapper.Map<ReadLivroNomeAutorDto>(livro);

        return Ok(livroDto);
    }

    /// <summary>
    /// Adiciona livro
    /// </summary>
    /// <param name="livroDto">Nome, AutorId, DataPuplicacao, Edicao, Editora, Isbn e Idioma</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso a inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionaLivro([FromBody] CreateLivroDto livroDto)
    {
        Livro livro = _mapper.Map<Livro>(livroDto);
        _context.Livros.Add(livro);
        _context.SaveChanges();

        return CreatedAtAction(nameof(BuscaLivroPorId), new { id = livro.Id }, livro);
    }

    /// <summary>
    /// Atualiza todos campos de livro
    /// </summary>
    /// <param name="id">Id do livro</param>
    /// <param name="livroDto">Nome, AutorId, DataPuplicacao, Edicao, Editora, Isbn e Idioma</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a atualização seja feita com sucesso</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult AtualizaLivro(int id, [FromBody] UpdateLivroDto livroDto)
    {
        var livro = _context.Livros.FirstOrDefault(Livro => Livro.Id == id);
        if (livro == null) return NotFound();

        _mapper.Map(livroDto, livro);
        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Atualiza um ou mais campos de livro
    /// </summary>
    /// <param name="id">Id do livro</param>
    /// <param name="patch">Nome, AutorId, DataPuplicacao, Edicao, Editora, Isbn e/ou Idioma</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a atualização seja feita com sucesso</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult AtualizaLivroParcial(int id, JsonPatchDocument<UpdateLivroDto> patch)
    {
        var livro = _context.Livros.FirstOrDefault(Livro => Livro.Id == id);
        if (livro == null) return NotFound();

        //Converte livro para UpdateLivroDto
        var livroParaAtualizar = _mapper.Map<UpdateLivroDto>(livro);

        //Valida livro
        patch.ApplyTo(livroParaAtualizar, ModelState);
        if (!TryValidateModel(livroParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(livroParaAtualizar, livro);
        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Deleta livro
    /// </summary>
    /// <param name="id">Id do livro</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a atualização seja feita com sucesso</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeletaLivro(int id)
    {
        var livro = _context.Livros.FirstOrDefault(Livro => Livro.Id == id);
        if (livro == null) return NotFound();

        _context.Remove(livro);
        _context.SaveChanges();

        return NoContent();
    }
}
