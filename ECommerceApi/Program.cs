
using Core.Interfaces;
using ECommerceApi.Helpers;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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


			builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));

			builder.Services.AddAutoMapper(typeof(MappingProfiles));

			//builder.Services.AddScoped<IProductRepository, ProductRepository>();
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseAuthorization();

			app.MapControllers();

			using var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider;
			var context = services.GetRequiredService<ApplicationDbContext>();
			var logger = services.GetRequiredService<ILogger<Program>>();
			try
			{
				context.Database.MigrateAsync();
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An error occurred during migration");
			}

			app.Run();
		}
	}
}
