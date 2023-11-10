using AutoMapper;
using BibliotecaWebAPI.Data;
using BibliotecaWebAPI.Data.Dtos;
using BibliotecaWebAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AutorController : ControllerBase
{
    private BibliotecaContext _context;
    private IMapper _mapper;

    public AutorController(BibliotecaContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Busca todos autores cadastrados
    /// </summary>
    /// <param name="take">Recupera x registros por consulta</param>
    /// <param name="skip">Pula x registros. Utilizado para paginação</param>
    /// <returns>IEnumerable ReadAutorDto</returns>
    /// <response code="200">Caso a consulta seja feita com sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadAutorDto> BuscaTodosAutores([FromQuery] int take = 20, [FromQuery] int skip = 0)
    {
        return _mapper.Map<List<ReadAutorDto>>(_context.Autores.Skip(skip).Take(take));
    }

    /// <summary>
    /// Busca autor por Id
    /// </summary>
    /// <param name="id">Id do autor</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a consulta seja feita com sucesso</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult BuscaAutorPorId(int id)
    {
        var autor = _context.Autores.FirstOrDefault(autor => autor.Id == id);
        if (autor == null) return NotFound();

        var autorDto = _mapper.Map<ReadAutorDto>(autor);

        return Ok(autorDto);
    }

    /// <summary>
    /// Adiciona autor
    /// </summary>
    /// <param name="autorDto">Nome, data de nascimento e nacionalidade</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso a inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionaAutor([FromBody] CreateAutorDto autorDto)
    {
        Autor autor = _mapper.Map<Autor>(autorDto);
        _context.Autores.Add(autor);
        _context.SaveChanges();

        return CreatedAtAction(nameof(BuscaAutorPorId), new { id = autor.Id }, autor);
    }

    /// <summary>
    /// Atualiza todos campos de autor
    /// </summary>
    /// <param name="id">Id do autor</param>
    /// <param name="autorDto">Nome, data de nascimento e nacionalidade</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a atualização seja feita com sucesso</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult AtualizaAutor(int id, [FromBody] UpdateAutorDto autorDto)
    {
        var autor = _context.Autores.FirstOrDefault(Autor => Autor.Id == id);
        if (autor == null) return NotFound();

        _mapper.Map(autorDto, autor);
        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Atualiza um ou mais campos de autor
    /// </summary>
    /// <param name="id">Id do autor</param>
    /// <param name="patch">Nome, data de nascimento e/ou nacionalidade</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a atualização seja feita com sucesso</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult AtualizaAutorParcial(int id, JsonPatchDocument<UpdateAutorDto> patch)
    {
        var autor = _context.Autores.FirstOrDefault(Autor => Autor.Id == id);
        if (autor == null) return NotFound();

        //Converte autor para UpdateAutorDto
        var autorParaAtualizar = _mapper.Map<UpdateAutorDto>(autor);

        //Valida autor
        patch.ApplyTo(autorParaAtualizar, ModelState);
        if (!TryValidateModel(autorParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(autorParaAtualizar, autor);
        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Deleta autor
    /// </summary>
    /// <param name="id">Id do autor</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a atualização seja feita com sucesso</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeletaAutor(int id)
    {
        var autor = _context.Autores.FirstOrDefault(Autor => Autor.Id == id);
        if (autor == null) return NotFound();

        _context.Remove(autor);
        _context.SaveChanges();

        return NoContent();
    }
}
