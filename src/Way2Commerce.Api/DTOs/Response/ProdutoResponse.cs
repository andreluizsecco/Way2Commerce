using Way2Commerce.Domain.Entities;

namespace Way2Commerce.Api.DTOs.Response
{
    public class ProdutoResponse
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataCadastro { get; set; }
        public CategoriaResponse Categoria { get; set; }
        
        public ProdutoResponse(int id, string codigo, string nome, string descricao, decimal preco, DateTime dataCadastro, CategoriaResponse categoria)
        {
            Id = id;
            Codigo = codigo;
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            DataCadastro = dataCadastro;
            Categoria = categoria;
        }

        public static ProdutoResponse ConverterParaResponse(Produto produto)
        {
            return new ProdutoResponse
            (
                produto.Id,
                produto.Codigo,
                produto.Nome,
                produto.Descricao,
                produto.Preco,
                produto.DataCadastro,
                new CategoriaResponse(produto.Categoria.Id, produto.Categoria.Nome)
            );
        }
    }
}