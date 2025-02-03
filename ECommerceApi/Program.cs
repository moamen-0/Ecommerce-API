
using Core.Entities.identity;
using Core.Interfaces;
using Core.IServices;
using ECommerceApi.Helpers;
using Infrastructure.Data;
using Infrastructure.Data._Identity;
using Infrastructure.Services;
using Infrastructure.Data._Identity.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.


			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
			  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			builder.Services.AddDbContext<StoreIdentityDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

			builder.Configuration.GetSection("JwtSettings").Bind(builder.Configuration.GetSection("JwtSettings").Value);



			builder.Services.AddIdentityCore<ApplicationUser>(opt=>
				{ }
				)
				.AddEntityFrameworkStores<StoreIdentityDbContext>()
				.AddSignInManager<SignInManager<ApplicationUser>>()				;
			builder.Services.AddAuthentication();
			builder.Services.AddAuthorization();
			builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
			{
				var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);
				return ConnectionMultiplexer.Connect(configuration);
			});
			builder.Services.AddScoped(typeof(IAuthService), typeof(AuthService));

			builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
			builder.Services.AddScoped(typeof(IBasketRepository),typeof(BasketRepository));

			builder.Services.AddAutoMapper(typeof(MappingProfiles));

			//builder.Services.AddScoped<IProductRepository, ProductRepository>();
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseStaticFiles();

			app.UseAuthorization();

			app.MapControllers();

			using var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider;
			var context = services.GetRequiredService<ApplicationDbContext>();
			var identityContext = services.GetRequiredService<StoreIdentityDbContext>();
			var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
			var logger = services.GetRequiredService<ILogger<Program>>();
			try
			{

				context.Database.MigrateAsync();
				identityContext.Database.MigrateAsync();
				//AppIdentityDbContextSeed.SeedUsersAsync(userManager).Wait();
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An error occurred during migration");
			}

			app.Run();
		}
	}
}
