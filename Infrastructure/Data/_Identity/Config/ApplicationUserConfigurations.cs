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
	internal class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder.Property(p => p.DisplayName).IsRequired();
			builder.HasOne(a => a.Address).WithOne().HasForeignKey<Address>(a => a.AppUserId).OnDelete(DeleteBehavior.Cascade);

		}

		
	
	}
}
