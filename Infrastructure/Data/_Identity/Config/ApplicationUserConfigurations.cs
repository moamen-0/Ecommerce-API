using Core.Entities.identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data._Identity.Config
{
	internal class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder.Property(p => p.DisplayName).IsRequired();


			builder.HasOne(u => u.Address)
			   .WithOne(a => a.AppUser)
			   .HasForeignKey<Address>(a => a.AppUserId)
			   .OnDelete(DeleteBehavior.Cascade);
		}
	}
}
