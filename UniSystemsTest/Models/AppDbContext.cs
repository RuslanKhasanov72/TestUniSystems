using Microsoft.EntityFrameworkCore;

namespace BuildingsApi.Models
{
	public class AppDbContext: DbContext
	{
		public DbSet<Building> Buildings { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options)
			 : base(options)
		{ }
	}
}
