using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductId)
               .IsRequired();

        builder.Property(x => x.Quantity)
               .IsRequired();

        builder.Property(x => x.Price)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

        builder.Property(x => x.CartId)
               .IsRequired();

        builder.HasOne(x => x.Product)
               .WithMany()
               .HasForeignKey(x => x.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
