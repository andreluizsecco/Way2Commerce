using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Way2Commerce.Api.Attributes;
using Way2Commerce.Api.Controllers.Shared;
using Way2Commerce.Application.DTOs.Request;
using Way2Commerce.Application.DTOs.Response;
using Way2Commerce.Domain.Interfaces.Services;
using Way2Commerce.Identity;

namespace Way2Commerce.Api.Controllers.v1;

[Authorize(Roles = Roles.Admin)]
[ApiVersion("1.0")]
public class ProdutoController : ApiControllerBase
{
    private IProdutoService _produtoService;

    public ProdutoController(IProdutoService produtoService) =>
        _produtoService = produtoService;

    /// <summary>
    /// Obtém todos os produtos.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <returns></returns>
    /// <response code="200">Retorna todos os produtos cadastrados</response>
    /// <response code="500">Retorna erros caso ocorram</response>
    [ProducesResponseType(typeof(IEnumerable<ProdutoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ClaimsAuthorize(ClaimTypes.Produto, "Ler")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoResponse>>> ObterTodos()
    {
        var produtos = await _produtoService.ObterTodosAsync();
        var produtosResponse = produtos.Select(produto => ProdutoResponse.ConverterParaResponse(produto));
        return Ok(produtosResponse);
    }

    /// <summary>
    /// Obtém produto por Id.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="id">Id do produto</param>
    /// <returns></returns>
    /// <response code="200">Retorna os dados do produto</response>
    /// <response code="404">Retorno caso o produto não seja encontrado</response>
    /// <response code="500">Retorna erros caso ocorram</response>
    [ProducesResponseType(typeof(IEnumerable<ProdutoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ClaimsAuthorize(ClaimTypes.Produto, "Ler")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoResponse>> ObterPorId(int id)
    {
        var produto = await _produtoService.ObterPorIdAsync(id);
        if (produto is null)
            return NotFound();
            
        var produtoResponse = ProdutoResponse.ConverterParaResponse(produto);
        return Ok(produtoResponse);
    }

    /// <summary>
    /// Insere um produto.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="produtoRequest">Dados do produto</param>
    /// <returns></returns>
    /// <response code="201">Retorna o Id do produto criado</response>
    /// <response code="400">Retorna erros de validação</response>
    /// <response code="500">Retorna erros caso ocorram</response>
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ClaimsAuthorizeAttribute(ClaimTypes.Produto, "Inserir")]
    [HttpPost]
    public async Task<ActionResult<int>> Inserir(InsercaoProdutoRequest produtoRequest)
    {
        var produto = InsercaoProdutoRequest.ConverterParaEntidade(produtoRequest);
        var id = (int)await _produtoService.AdicionarAsync(produto);
        return CreatedAtAction(nameof(ObterPorId), new { id = id }, id);
    }

    /// <summary>
    /// Atualiza um produto.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="produtoRequest">Dados do produto</param>
    /// <returns></returns>
    /// <response code="200">Sucesso ao atualizar produto</response>
    /// <response code="400">Retorna erros de validação</response>
    /// <response code="500">Retorna erros caso ocorram</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ClaimsAuthorizeAttribute(ClaimTypes.Produto, "Atualizar")]
    [HttpPut]
    public async Task<ActionResult> Atualizar(AtualizacaoProdutoRequest produtoRequest)
    {
        var produto = AtualizacaoProdutoRequest.ConverterParaEntidade(produtoRequest);
        await _produtoService.AtualizarAsync(produto);
        return Ok();
    }

    /// <summary>
    /// Exclui um produto.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="id">Id do produto</param>
    /// <returns></returns>
    /// <response code="200">Sucesso ao excluir produto</response>
    /// <response code="400">Retorna erros de validação</response>
    /// <response code="500">Retorna erros caso ocorram</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [Authorize(Policy = Policies.HorarioComercial)]
    [ClaimsAuthorizeAttribute(ClaimTypes.Produto, "Excluir")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Excluir(int id)
    {
        await _produtoService.RemoverPorIdAsync(id);
        return Ok();
    }
}