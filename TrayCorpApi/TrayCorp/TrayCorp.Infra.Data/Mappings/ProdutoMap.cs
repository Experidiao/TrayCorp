using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrayCorp.Domain.Models;

namespace TrayCorp.Infra.Data.Mappings
{
    public class ProdutoMap :IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(c => new { c.IdProduto });
            builder.Property(c => c.Nome).HasMaxLength(60).IsRequired();
            builder.Property(c => c.Estoque).IsRequired();
            builder.Property(c => c.Valor).IsRequired();

        }

    }
}
