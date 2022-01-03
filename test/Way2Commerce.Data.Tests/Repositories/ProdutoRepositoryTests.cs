using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Way2Commerce.Data.Context;
using Way2Commerce.Data.Repositories;
using Way2Commerce.Data.Tests.Database;
using Way2Commerce.Domain.Entities;
using Xunit;

namespace Way2Commerce.Data.Tests.Repositories
{
    public class ProdutoRepositoryTests
    {
        private readonly DataContext _dataContext;
        private readonly DBInMemory _dbInMemory;
        private readonly ProdutoRepository _produtoRepository;

        public ProdutoRepositoryTests()
        {
            _dbInMemory = new DBInMemory();
            _dataContext = _dbInMemory.GetContext();

            _produtoRepository = new ProdutoRepository(_dataContext);
        }

        [Fact]
        public async Task ObterTodosAsync_Deve_Retornar_Todos_Os_Registros()
        {
            var produtos = await _produtoRepository.ObterTodosAsync();
            produtos.Should().HaveCount(4);
            produtos.FirstOrDefault().Categoria.Should().NotBeNull();
        }

        [Fact]
        public async Task ObterPorIdAsync_Deve_Retornar_Registro_Com_O_Id_Especificado()
        {
            var id = 2;
            var produto = await _produtoRepository.ObterPorIdAsync(id);
            produto.Id.Should().Be(id);
            produto.Categoria.Should().NotBeNull();
        }

        [Fact]
        public async Task AdicionarAsync_Deve_Adicionar_Produto_E_Retornar_Id()
        {
            var produto = new Produto("000005", 4, "Produto5", "Descrição5", 50.00m);
            var id = await _produtoRepository.AdicionarAsync(produto);
            id.Should().Be(produto.Id);
        }

        [Fact]
        public async Task AtualizarAsync_Deve_Atualizar_Produto()
        {
            var novoPreco = 109.99m;
            
            var produto = await _produtoRepository.ObterPorIdAsync(1);
            produto.Preco = novoPreco;
            await _produtoRepository.AtualizarAsync(produto);

            produto = await _produtoRepository.ObterPorIdAsync(1);
            produto.Preco.Should().Be(novoPreco);
        }

        [Fact]
        public async Task RemoverAsync_Deve_Remover_Produto()
        {
            var id = 3;
            var produto = await _produtoRepository.ObterPorIdAsync(id);
            await _produtoRepository.RemoverAsync(produto);

            var produtoExcluido = await _produtoRepository.ObterPorIdAsync(id);
            produtoExcluido.Should().BeNull();
        }

        [Fact]
        public async Task RemoverPorIdAsync_Deve_Remover_Produto()
        {
            var id = 3;
            await _produtoRepository.RemoverPorIdAsync(id);

            var produtoExcluido = await _produtoRepository.ObterPorIdAsync(id);
            produtoExcluido.Should().BeNull();
        }

        [Fact]
        public async Task RemoverPorIdAsync_Deve_Lancar_Excecao_Para_Registro_Inexistente()
        {
            var id = 100;
            await FluentActions.Invoking(async () => await _produtoRepository.RemoverPorIdAsync(id))
                .Should().ThrowAsync<Exception>("O registro não existe na base de dados.");
        }
    }
}