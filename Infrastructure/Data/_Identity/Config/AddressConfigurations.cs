using Core.Entities.identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data._Identity.Config
{
	public class AddressConfigurations : IEntityTypeConfiguration<Address>
	{
		public void Configure(EntityTypeBuilder<Address> builder)
		{
			builder.ToTable("Addresses");
			builder.Property(p => p.FirstName).IsRequired();
			builder.Property(p => p.LastName).IsRequired();
			builder.Property(p => p.Street).IsRequired();
			builder.Property(p => p.City).IsRequired();
			builder.Property(p => p.State).IsRequired();
			builder.Property(p => p.Zipcode).IsRequired();

			builder.HasOne(a => a.AppUser)
				   .WithOne(u => u.Address)
				   .HasForeignKey<Address>(a => a.AppUserId)
				   .OnDelete(DeleteBehavior.Cascade);


		}
	}
}
