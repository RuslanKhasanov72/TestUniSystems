using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoomsApi.Interfaces;
using RoomsApi.Models;
using RoomsApi.Services;

namespace RoomsApi.Controllers
{
	/// <summary>
	/// Контроллер для управления зданиями.
	/// </summary>
	/// <remarks>
	/// Этот контроллер предоставляет API для получения информации о зданиях в базе данных комнат.
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
		/// <response code="200">Возвращает список зданий.</response>
		[HttpGet]
		[Route("GetAll")]
		public async Task<ActionResult<IEnumerable<Building>>> GetBuildings()
		{
			var buildings = await _buildingService.GetAllAsync();
			return Ok(buildings);
		}
	}
}
