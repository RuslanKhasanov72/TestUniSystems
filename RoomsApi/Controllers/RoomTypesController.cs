using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoomsApi.Interfaces;
using RoomsApi.Models;
using RoomsApi.Services;

namespace RoomsApi.Controllers
{
	/// <summary>
	/// Контроллер для управления типами комнат.
	/// </summary>
	/// <remarks>
	/// Этот контроллер предоставляет API для получения всех доступных типов комнат.
	/// </remarks>
	[Route("api/[controller]")]
	[ApiController]
	public class RoomTypesController : ControllerBase
	{
		private readonly RoomTypeService _roomtypeService;

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="RoomTypesController"/> с указанным сервисом типов комнат.
		/// </summary>
		/// <param name="roomtypeService">Сервис для работы с типами комнат.</param>
		public RoomTypesController(RoomTypeService roomtypeService)
		{
			_roomtypeService = roomtypeService;
		}

		/// <summary>
		/// Получает все доступные типы комнат.
		/// </summary>
		/// <returns>Список типов комнат.</returns>
		/// <response code="200">Возвращает список типов комнат.</response>
		[HttpGet]
		[Route("GetAll")]
		public async Task<ActionResult<IEnumerable<RoomType>>> GetRoomTypes()
		{
			var roomTypes = await _roomtypeService.GetAllAsync();
			return Ok(roomTypes);
		}
	}
}
