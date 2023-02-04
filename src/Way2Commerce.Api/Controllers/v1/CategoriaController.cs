using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Way2Commerce.Api.Controllers.Shared;
using Way2Commerce.Application.DTOs.Response;
using Way2Commerce.Domain.Interfaces.Repositories;

namespace Way2Commerce.Api.Controllers.v1;

[Authorize]
[ApiVersion("1.0")]
public class CategoriaController : ApiControllerBase
{
    private ICategoriaRepository _categoriaRepository;

    public CategoriaController(ICategoriaRepository categoriaRepository) =>
        _categoriaRepository = categoriaRepository;

    /// <summary>
    /// Obtém todas as categorias.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <returns></returns>
    /// <response code="200">Retorna todas as categorias cadastradas</response>
    /// <response code="500">Retorna erros caso ocorram</response>
    [ProducesResponseType(typeof(IEnumerable<CategoriaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaResponse>>> ObterTodas()
    {
        var categorias = await _categoriaRepository.ObterTodosAsync();
        var categoriasResponse = categorias.Select(categoria => CategoriaResponse.ConverterParaResponse(categoria));
        return Ok(categoriasResponse);
    }

    
    /// <summary>
    /// Obtém categoria por Id.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="id">Id da categoria</param>
    /// <returns></returns>
    /// <response code="200">Retorna os dados da categoria</response>
    /// <response code="404">Retorno caso a categoria não seja encontrada</response>
    /// <response code="500">Retorna erros caso ocorram</response>
    [ProducesResponseType(typeof(IEnumerable<CategoriaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoriaResponse>> ObterPorId(int id)
    {
        var categoria = await _categoriaRepository.ObterPorIdAsync(id);
        if (categoria is null)
            return NotFound();
            
        var categoriaResponse = CategoriaResponse.ConverterParaResponse(categoria);
        return Ok(categoriaResponse);
    }
}