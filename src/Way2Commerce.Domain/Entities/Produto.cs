using Way2Commerce.Domain.Entities.Shared;

namespace Way2Commerce.Domain.Entities
{
    public class Produto : Entity
    {
        public string Codigo { get; private set; }
        public int IdCategoria { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; set; }
        public DateTime DataCadastro { get; private set; }
        
        public Categoria Categoria { get; private set; }

        public Produto(int id, string codigo, int idCategoria, string nome, string descricao, decimal preco)
        {
            Id = id;
            Codigo = codigo;
            IdCategoria = idCategoria;
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            DataCadastro = DateTime.Now;
        }

        public Produto(string codigo, int idCategoria, string nome, string descricao, decimal preco)
            : this(default, codigo, idCategoria, nome, descricao, preco) { }
                
    }
}