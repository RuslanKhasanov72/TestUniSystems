using BuildingsApi.Models;
using BuildingsApi.Services;
using BuildingsApi.EditModels;
using Microsoft.AspNetCore.Mvc;

namespace BuildingsApi.Controllers
{
	/// <summary>
	/// Контроллер для управления зданиями.
	/// </summary>
	/// <remarks>
	/// Этот контроллер предоставляет API для управления зданиями в системе.
	/// Вы можете получать информацию о всех зданиях, добавлять новые здания,
	/// обновлять существующие и удалять здания.
	/// </remarks>
	[Route("api/[controller]")]
	[ApiController]
	public class BuildingsController : ControllerBase
	{
		private readonly BuildingService _buildingService;

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="BuildingsController"/> с указанным сервисом зданий.
		/// </summary>
		/// <param name="buildingService">Сервис для работы с зданиями.</param>
		public BuildingsController(BuildingService buildingService)
		{
			_buildingService = buildingService;
		}

		/// <summary>
		/// Получает все здания.
		/// </summary>
		/// <returns>Список зданий.</returns>
		[HttpGet]
		[Route("GetAll")]
		public async Task<ActionResult<IEnumerable<Building>>> GetBuildings()
		{
			var buildings = await _buildingService.GetAllBuildingsAsync();
			return Ok(buildings);
		}

		/// <summary>
		/// Получает здание по идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор здания.</param>
		/// <returns>Здание с указанным идентификатором, если оно существует.</returns>
		[HttpGet]
		[Route("GetBy/{id:int}")]
		public async Task<ActionResult<Building>> GetBuilding(int id)
		{
			var building = await _buildingService.GetBuildingByIdAsync(id);
			if (building == null)
			{
				return NotFound();
			}
			return Ok(building);
		}

		/// <summary>
		/// Создает новое здание.
		/// </summary>
		/// <param name="editbuilding">Данные для создания нового здания.</param>
		/// <returns>Созданное здание.</returns>
		[HttpPost]
		[Route("Add")]
		public async Task<ActionResult<Building>> CreateBuilding(EditBuilding editbuilding)
		{
			var building = new Building
			{
				Name = editbuilding.Name,
				Address = editbuilding.Address,
				NumberOfFloors = editbuilding.NumberOfFloors
			};

			var createdBuilding = await _buildingService.CreateBuildingAsync(building);
			_buildingService.SendBuildingMessage(createdBuilding, "update");

			return CreatedAtAction(nameof(GetBuilding), new { id = createdBuilding.Id }, createdBuilding);
		}

		/// <summary>
		/// Обновляет существующее здание.
		/// </summary>
		/// <param name="id">Идентификатор здания, которое необходимо обновить.</param>
		/// <param name="editbuilding">Данные для обновления здания.</param>
		/// <returns>Результат обновления.</returns>
		[HttpPut("Update/{id}")]
		public async Task<IActionResult> UpdateBuilding(int id, EditBuilding editbuilding)
		{
			var building = await _buildingService.GetBuildingByIdAsync(id);
			if (building == null)
			{
				return NotFound();
			}

			building.Name = editbuilding.Name;
			building.Address = editbuilding.Address;
			building.NumberOfFloors = editbuilding.NumberOfFloors;

			await _buildingService.UpdateBuildingAsync(building);
			_buildingService.SendBuildingMessage(building, "update");

			return NoContent();
		}

		/// <summary>
		/// Удаляет здание.
		/// </summary>
		/// <param name="id">Идентификатор здания, которое необходимо удалить.</param>
		/// <returns>Результат удаления.</returns>
		[HttpDelete("Delete{id}")]
		public async Task<IActionResult> DeleteBuilding(int id)
		{
			var building = await _buildingService.GetBuildingByIdAsync(id);
			if (building == null)
			{
				return NotFound();
			}

			await _buildingService.DeleteBuildingAsync(building);
			_buildingService.SendBuildingMessage(building, "delete");

			return NoContent();
		}
	}
}