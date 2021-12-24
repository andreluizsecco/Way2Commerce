using Microsoft.AspNetCore.Mvc;
using Way2Commerce.Api.Controllers.Shared;
using Way2Commerce.Api.DTOs.Response;
using Way2Commerce.Domain.Interfaces.Repositories;

namespace Way2Commerce.Api.Controllers.v1;

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
        return Ok(categorias);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoriaResponse>> ObterPorId(int id)
    {
        var categoria = await _categoriaRepository.ObterPorIdAsync(id);
        return Ok(categoria);
    }
}