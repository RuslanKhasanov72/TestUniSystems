using BuildingsApi.Interfaces;
using BuildingsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildingsApi.Repositories
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

		public async Task<Building> GetByIdAsync(int id)
		{
			return await _context.Buildings.FindAsync(id);
		}

		public async Task<Building> CreateAsync(Building building)
		{
			await _context.Buildings.AddAsync(building);
			await _context.SaveChangesAsync();
			return building;
		}

		public async Task UpdateAsync(Building building)
		{
			_context.Buildings.Update(building);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Building building)
		{
			_context.Buildings.Remove(building);
			await _context.SaveChangesAsync();
		}
	}
}