using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Way2Commerce.Domain.Entities;

namespace Way2Commerce.Data.Mappings;

public class ProdutoMap : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produto");

        builder.Property(p => p.Codigo)
            .HasMaxLength(6)
            .IsUnicode(false);
        
        builder.Property(p => p.Nome)
            .HasMaxLength(100)
            .IsUnicode(false);

        builder.Property(p => p.Preco)
            .HasPrecision(18, 2);

        builder.HasOne(p => p.Categoria)
            .WithMany(p => p.Produtos)
            .HasForeignKey(p => p.IdCategoria)
            .OnDelete(DeleteBehavior.NoAction);
    }
}