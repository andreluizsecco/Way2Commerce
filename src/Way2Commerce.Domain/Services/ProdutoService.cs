using Way2Commerce.Domain.Entities;
using Way2Commerce.Domain.Interfaces.Repositories;
using Way2Commerce.Domain.Interfaces.Services;
using Way2Commerce.Domain.Services.Shared;

namespace Way2Commerce.Domain.Services;

public class ProdutoService : ServiceBase<Produto>, IProdutoService
{
    public ProdutoService(IProdutoRepository produtoRepository) : base(produtoRepository) { }
}