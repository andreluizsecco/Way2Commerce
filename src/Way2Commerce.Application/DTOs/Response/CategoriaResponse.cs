using Way2Commerce.Domain.Entities;

namespace Way2Commerce.Application.DTOs.Response;

public class CategoriaResponse
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public CategoriaResponse(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }

    public static CategoriaResponse ConverterParaResponse(Categoria categoria)
    {
        return new CategoriaResponse
        (
            categoria.Id,
            categoria.Nome
        );
    }
}