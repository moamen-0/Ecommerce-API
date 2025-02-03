using Core.Entities.identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data._Identity
{
	public class StoreIdentityDbContext : IdentityDbContext<ApplicationUser>
	{


		public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options) : base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(typeof(StoreIdentityDbContext).Assembly);

		}




	}
	
}
