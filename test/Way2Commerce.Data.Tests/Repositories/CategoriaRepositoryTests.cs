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
    public class CategoriaRepositoryTests
    {
        private readonly DataContext _dataContext;
        private readonly DBInMemory _dbInMemory;
        private readonly CategoriaRepository _categoriaRepository;

        public CategoriaRepositoryTests()
        {
            _dbInMemory = new DBInMemory();
            _dataContext = _dbInMemory.GetContext();

            _categoriaRepository = new CategoriaRepository(_dataContext);
        }

        [Fact]
        public async Task ObterTodosAsync_Deve_Retornar_Todos_Os_Registros()
        {
            var categorias = await _categoriaRepository.ObterTodosAsync();
            categorias.Should().HaveCount(4);
        }

        [Fact]
        public async Task ObterPorIdAsync_Deve_Retornar_Registro_Com_O_Id_Especificado()
        {
            var id = 2;
            var categoria = await _categoriaRepository.ObterPorIdAsync(id);
            categoria.Id.Should().Be(id);
        }
    }
}