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

    [ClaimsAuthorize(ClaimTypes.Produto, "Ler")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoResponse>>> ObterTodos()
    {
        var produtos = await _produtoService.ObterTodosAsync();
        var produtosResponse = produtos.Select(produto => ProdutoResponse.ConverterParaResponse(produto));
        return Ok(produtosResponse);
    }

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

    [ClaimsAuthorizeAttribute(ClaimTypes.Produto, "Inserir")]
    [HttpPost]
    public async Task<ActionResult<int>> Inserir(InsercaoProdutoRequest produtoRequest)
    {
        var produto = InsercaoProdutoRequest.ConverterParaEntidade(produtoRequest);
        var id = (int)await _produtoService.AdicionarAsync(produto);
        return CreatedAtAction(nameof(ObterPorId), new { id = id }, id);
    }

    [ClaimsAuthorizeAttribute(ClaimTypes.Produto, "Atualizar")]
    [HttpPut]
    public async Task<ActionResult> Atualizar(AtualizacaoProdutoRequest produtoRequest)
    {
        var produto = AtualizacaoProdutoRequest.ConverterParaEntidade(produtoRequest);
        await _produtoService.AtualizarAsync(produto);
        return Ok();
    }

    [Authorize(Policy = Policies.HorarioComercial)]
    [ClaimsAuthorizeAttribute(ClaimTypes.Produto, "Excluir")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Excluir(int id)
    {
        await _produtoService.RemoverPorIdAsync(id);
        return Ok();
    }
}