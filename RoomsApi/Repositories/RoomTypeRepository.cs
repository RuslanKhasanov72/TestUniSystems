using Microsoft.EntityFrameworkCore;
using RoomsApi.Interfaces;
using RoomsApi.Models;

namespace RoomsApi.Repositories
{
	public class RoomTypeRepository : IRoomTypeRepository
	{
		private readonly AppDbContext _context;

		public RoomTypeRepository(AppDbContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<RoomType>> GetAllAsync()
		{
			return await _context.RoomTypes.ToListAsync();
		}
	}
}
