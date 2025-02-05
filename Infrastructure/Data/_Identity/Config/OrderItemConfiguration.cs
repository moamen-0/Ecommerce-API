using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data._Identity.Config
{
	public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.OwnsOne(i => i.ProductItemOrder, io =>
			{
				io.WithOwner();
			});
			builder.Property(i => i.Price)
				.HasColumnType("decimal(18,2)");
			builder.Property(i => i.Quantity)
				.HasColumnType("decimal(18,2)");


		}

	}
}
