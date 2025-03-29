using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
               .IsRequired();

        builder.Property(x => x.TotalPrice)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

        builder.Property(x => x.PaymentMethod)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(x => x.ShippingAddress)
               .IsRequired()
               .HasMaxLength(250);

        builder.Property(x => x.Status)
               .IsRequired();

        builder.HasMany(x => x.OrderItems)
               .WithOne(x => x.Order)
               .HasForeignKey(x => x.OrderId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
