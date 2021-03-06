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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaResponse>>> ObterTodas()
    {
        var categorias = await _categoriaRepository.ObterTodosAsync();
        var categoriasResponse = categorias.Select(categoria => CategoriaResponse.ConverterParaResponse(categoria));
        return Ok(categoriasResponse);
    }

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