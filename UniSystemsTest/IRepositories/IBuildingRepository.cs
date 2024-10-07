using BuildingsApi.Models;

namespace BuildingsApi.Interfaces
{
	public interface IBuildingRepository
	{
		Task<IEnumerable<Building>> GetAllAsync();
		Task<Building> CreateAsync(Building building);
		Task<Building> GetByIdAsync(int id);
		Task UpdateAsync(Building building);
		Task DeleteAsync(Building building);
	}
}
