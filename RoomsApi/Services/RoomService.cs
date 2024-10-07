using RoomsApi.EditModels;
using RoomsApi.Interfaces;
using RoomsApi.Models;

namespace RoomsApi.Services
{
	public class RoomService
	{
		private readonly IRoomRepository _roomRepository;

		public RoomService(IRoomRepository roomRepository)
		{
			_roomRepository = roomRepository;
		}

		public async Task<IEnumerable<ShowRoom>> GetAllAsync()
		{
			return await _roomRepository.GetAllAsync();
		}

		public async Task<Room> GetByIdAsync(int id)
		{
			return await _roomRepository.GetByIdAsync(id);
		}

		public async Task<Room> CreateAsync(Room room)
		{
			return await _roomRepository.CreateAsync(room);
		}

		public async Task UpdateAsync(Room room)
		{
			await _roomRepository.UpdateAsync(room);
		}

		public async Task DeleteAsync(Room room)
		{
			await _roomRepository.DeleteAsync(room);
		}

		public async Task<Building> GetBuildingByIdAsync(int buildingId)
		{ 
			return await _roomRepository.GetBuildingByIdAsync(buildingId);
		}
	}
}