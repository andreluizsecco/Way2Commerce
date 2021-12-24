using Way2Commerce.Data.Context;
using Way2Commerce.Data.Repositories.Shared;
using Way2Commerce.Domain.Entities;
using Way2Commerce.Domain.Interfaces.Repositories;

namespace Way2Commerce.Data.Repositories;

public class CategoriaRepository : RepositoryBase<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(DataContext dataContext) : base(dataContext) { }
}