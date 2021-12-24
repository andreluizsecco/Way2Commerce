using Way2Commerce.Domain.Entities;

namespace Way2Commerce.Api.DTOs.Request
{
    public class InsercaoProdutoRequest
    {
        public string Codigo { get; set; }
        public int IdCategoria { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        
        public InsercaoProdutoRequest(string codigo, int idCategoria, string nome, string descricao, decimal preco)
        {
            Codigo = codigo;
            IdCategoria = idCategoria;
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
        }

        public static Produto ConverterParaEntidade(InsercaoProdutoRequest produtoRequest)
        {
            return new Produto
            (
                produtoRequest.Codigo,
                produtoRequest.IdCategoria,
                produtoRequest.Nome,
                produtoRequest.Descricao,
                produtoRequest.Preco
            );
        }
    }
}