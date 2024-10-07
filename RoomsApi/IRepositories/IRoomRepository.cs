using RoomsApi.EditModels;
using RoomsApi.Models;

namespace RoomsApi.Interfaces
{
	public interface IRoomRepository
	{
		Task<IEnumerable<ShowRoom>> GetAllAsync();
		Task<Room> GetByIdAsync(int id);
		Task<Room> CreateAsync(Room room);
		Task UpdateAsync(Room room);
		Task DeleteAsync(Room room);
		Task<Building> GetBuildingByIdAsync(int buildingId);
	}
}