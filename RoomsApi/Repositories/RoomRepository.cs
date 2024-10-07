using RoomsApi.Models;
using Microsoft.EntityFrameworkCore;
using RoomsApi.Interfaces;
using RoomsApi.EditModels;

namespace RoomsApi.Repositories
{
	public class RoomRepository : IRoomRepository
	{
		private readonly AppDbContext _context;

		public RoomRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<ShowRoom>> GetAllAsync()
		{
			return await _context.Rooms.Select(room => new ShowRoom
			{
				Id = room.Id,
				Name = room.Name,
				BuildingName = _context.Buildings.FirstOrDefault(b => b.Id == room.BuildingId).Name,
				RoomTypeName = _context.RoomTypes.FirstOrDefault(b => b.Id == room.RoomTypeId).Name,
				Capacity = room.Capacity,
				Floor = room.Floor,
				Number = room.Number
			}).ToListAsync();
		}

		public async Task<Room> GetByIdAsync(int id)
		{
			return await _context.Rooms.Include(r => r.Building).Include(r => r.RoomType).FirstOrDefaultAsync(r => r.Id == id);
		}

		public async Task<Room> CreateAsync(Room room)
		{
			_context.Rooms.Add(room);
			await _context.SaveChangesAsync();
			return room;
		}

		public async Task UpdateAsync(Room room)
		{
			_context.Rooms.Update(room);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Room room)
		{
			_context.Rooms.Remove(room);
			await _context.SaveChangesAsync();
		}
		public async Task<Building> GetBuildingByIdAsync(int buildingId)
		{
			return await _context.Buildings.FirstOrDefaultAsync(b => b.Id == buildingId);
		}
	}
}