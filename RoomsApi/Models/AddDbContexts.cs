using Microsoft.EntityFrameworkCore;

namespace RoomsApi.Models
{
	public class AppDbContext:DbContext
	{
		public DbSet<Room> Rooms { get; set; }
		public DbSet<RoomType> RoomTypes { get; set; }
		public DbSet<Building> Buildings { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options)
			 : base(options)
		{ }
	}
}
