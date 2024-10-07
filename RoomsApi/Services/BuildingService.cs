using RoomsApi.Interfaces;
using RoomsApi.Models;
using RoomsApi.Repositories;

namespace RoomsApi.Services
{
	public class BuildingService
	{
		private readonly IBuildingRepository _buildingRepository;
		public BuildingService(IBuildingRepository buildingRepository)
		{
			_buildingRepository = buildingRepository;
		}
		public async Task<IEnumerable<Building>> GetAllAsync()
		{
			return await _buildingRepository.GetAllAsync();
		}
	}
}
