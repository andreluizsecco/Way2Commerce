using Microsoft.EntityFrameworkCore;
using Way2Commerce.Data.Context;
using Way2Commerce.Data.Repositories.Shared;
using Way2Commerce.Domain.Entities;
using Way2Commerce.Domain.Interfaces.Repositories;

namespace Way2Commerce.Data.Repositories;

public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
{
    public ProdutoRepository(DataContext dataContext) : base(dataContext) { }

    public async override Task<IEnumerable<Produto>> ObterTodosAsync()
    {
        return await Context.Produtos
            .Include(p => p.Categoria)
            .AsNoTracking()
            .ToListAsync();
    }

    public async override Task<Produto?> ObterPorIdAsync(int id)
    {
        return await Context.Produtos
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.Id.Equals(id));
    }
}