using RoomsApi.Models;

namespace RoomsApi.Interfaces
{
	public interface IBuildingRepository
	{
		Task<IEnumerable<Building>> GetAllAsync();
	}
}