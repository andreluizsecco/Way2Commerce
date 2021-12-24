using Way2Commerce.Domain.Entities.Shared;

namespace Way2Commerce.Domain.Entities
{
    public class Categoria : Entity
    {
        public string Nome { get; set; }

        public ICollection<Produto> Produtos { get; private set; }
        
        public Categoria(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}