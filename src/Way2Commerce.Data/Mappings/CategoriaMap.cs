using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Way2Commerce.Domain.Entities;

namespace Way2Commerce.Data.Mappings;

public class CategoriaMap : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("Categoria");

        builder.Property(p => p.Id)
            .ValueGeneratedNever();

        builder.Property(p => p.Nome)
            .HasMaxLength(100)
            .IsUnicode(false);

        builder.HasData(new[]
        {
            new Categoria(1, "Eletrodomésticos"),
            new Categoria(2, "Informática"),
            new Categoria(3, "Vestuário"),
            new Categoria(4, "Livros")
        });
    }
}