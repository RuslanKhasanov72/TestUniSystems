using Microsoft.EntityFrameworkCore;
using RoomsApi.Interfaces;
using RoomsApi.Models;

namespace RoomsApi.Repositories
{
	public class BuildingRepository : IBuildingRepository
	{
		private readonly AppDbContext _context;

		public BuildingRepository(AppDbContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<Building>> GetAllAsync()
		{
			return await _context.Buildings.ToListAsync();
		}
	}
}
