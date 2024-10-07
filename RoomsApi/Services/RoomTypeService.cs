using RoomsApi.Interfaces;
using RoomsApi.Models;
using RoomsApi.Repositories;

namespace RoomsApi.Services
{
	public class RoomTypeService
	{
		private readonly IRoomTypeRepository _roomtypeRepository;
		public RoomTypeService(IRoomTypeRepository roomtypeRepository)
		{
			_roomtypeRepository = roomtypeRepository;
		}
		public async Task<IEnumerable<RoomType>> GetAllAsync()
		{
			return await _roomtypeRepository.GetAllAsync();
		}
	}
}
