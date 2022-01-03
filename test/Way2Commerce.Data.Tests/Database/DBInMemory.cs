using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Way2Commerce.Data.Context;
using Way2Commerce.Domain.Entities;

namespace Way2Commerce.Data.Tests.Database
{
    public class DBInMemory
    {
        private readonly DataContext _dataContext;
        private readonly SqliteConnection _connection;

        public DBInMemory()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlite(_connection)
                .EnableSensitiveDataLogging()
                .Options;

            _dataContext = new DataContext(options);
            InsertFakeData();
        }

        public DataContext GetContext() => _dataContext;

        public void Cleanup() =>
            _connection.Close();

        private void InsertFakeData()
        {
            if (_dataContext.Database.EnsureCreated())
            {
                var idsProdutos = new[] { 1, 2, 3, 4 };
                
                idsProdutos.ToList().ForEach(id =>
                {
                    _dataContext.Produtos.Add(
                        new Produto(id, $"00000{id}", 1, $"Produto{id}", $"Descrição{id}", 10* id)
                    );
                });

                _dataContext.SaveChanges();
            }
        }
    }
}