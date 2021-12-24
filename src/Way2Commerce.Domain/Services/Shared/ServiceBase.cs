using Way2Commerce.Domain.Entities.Shared;
using Way2Commerce.Domain.Interfaces.Repositories.Shared;
using Way2Commerce.Domain.Interfaces.Services.Shared;

namespace Way2Commerce.Domain.Services.Shared;

public abstract class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : Entity
{
    private readonly IRepositoryBase<TEntity> _repositoryBase;

    public ServiceBase(IRepositoryBase<TEntity> repositoryBase) =>
        _repositoryBase = repositoryBase;

    public virtual async Task<IEnumerable<TEntity>> ObterTodosAsync() =>
        await _repositoryBase.ObterTodosAsync();

    public virtual async Task<TEntity?> ObterPorIdAsync(int id) =>
        await _repositoryBase.ObterPorIdAsync(id);

    public virtual async Task<object> AdicionarAsync(TEntity objeto) =>
        await _repositoryBase.AdicionarAsync(objeto);

    public virtual async Task AtualizarAsync(TEntity objeto) =>
        await _repositoryBase.AtualizarAsync(objeto);
    
    public virtual async Task RemoverAsync(TEntity objeto) =>
        await _repositoryBase.RemoverAsync(objeto);

    public virtual async Task RemoverPorIdAsync(int id) =>
        await _repositoryBase.RemoverPorIdAsync(id);

    public void Dispose() =>
        _repositoryBase.Dispose();
}