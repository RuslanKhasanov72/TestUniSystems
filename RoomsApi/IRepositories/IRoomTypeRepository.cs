using RoomsApi.Models;

namespace RoomsApi.Interfaces
{
	public interface IRoomTypeRepository
	{
		Task<IEnumerable<RoomType>> GetAllAsync();
	}
}