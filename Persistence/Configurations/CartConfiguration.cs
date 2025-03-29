using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
               .IsRequired()
               .HasMaxLength(450);

        builder.HasMany(x => x.CartItems)
               .WithOne(x => x.Cart)
               .HasForeignKey(x => x.CartId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
