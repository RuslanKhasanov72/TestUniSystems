using Microsoft.AspNetCore.Mvc;
using RoomsApi.EditModels;
using RoomsApi.Interfaces;
using RoomsApi.Models;
using RoomsApi.Services;

namespace RoomsApi.Controllers
{
	/// <summary>
	/// Контроллер для управления комнатами.
	/// </summary>
	/// <remarks>
	/// Этот контроллер предоставляет API для получения, создания, обновления и удаления комнат.
	/// </remarks>
	[Route("api/[controller]")]
	[ApiController]
	public class RoomsController : ControllerBase
	{
		private readonly RoomService _roomService;

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="RoomsController"/> с указанным сервисом комнат.
		/// </summary>
		/// <param name="roomService">Сервис для работы с комнатами.</param>
		public RoomsController(RoomService roomService)
		{
			_roomService = roomService;
		}

		/// <summary>
		/// Получает все комнаты.
		/// </summary>
		/// <returns>Список комнат.</returns>
		/// <response code="200">Возвращает список комнат.</response>
		[HttpGet]
		[Route("GetAll")]
		public async Task<ActionResult<IEnumerable<ShowRoom>>> GetRooms()
		{
			var rooms = await _roomService.GetAllAsync();
			return Ok(rooms);
		}

		/// <summary>
		/// Получает комнату по идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор комнаты.</param>
		/// <returns>Комната с указанным идентификатором.</returns>
		/// <response code="200">Возвращает комнату.</response>
		/// <response code="404">Комната не найдена.</response>
		[HttpGet]
		[Route("GetBy/{id:int}")]
		public async Task<ActionResult<Room>> GetRoom(int id)
		{
			var room = await _roomService.GetByIdAsync(id);
			if (room == null)
			{
				return NotFound();
			}

			return Ok(room);
		}

		/// <summary>
		/// Создает новую комнату.
		/// </summary>
		/// <param name="editroom">Модель данных для создания комнаты.</param>
		/// <returns>Созданная комната.</returns>
		/// <response code="201">Комната успешно создана.</response>
		/// <response code="404">Корпус не найден.</response>
		[HttpPost]
		[Route("Add")]
		public async Task<ActionResult<Room>> CreateRoom(EditRoom editroom)
		{
			var building = await _roomService.GetBuildingByIdAsync(editroom.BuildingId);
			if (building == null)
			{
				return NotFound($"Корпус с Id = {editroom.BuildingId} не существует.");
			}

			var room = new Room
			{
				Name = editroom.Name,
				RoomTypeId = editroom.RoomTypeId,
				BuildingId = editroom.BuildingId,
				Capacity = editroom.Capacity,
				Floor = editroom.Floor,
				Number = editroom.Number
			};

			await _roomService.CreateAsync(room);
			return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
		}

		/// <summary>
		/// Обновляет существующую комнату.
		/// </summary>
		/// <param name="id">Идентификатор комнаты.</param>
		/// <param name="editroom">Модель данных для обновления комнаты.</param>
		/// <returns>Статус операции.</returns>
		/// <response code="204">Комната успешно обновлена.</response>
		/// <response code="404">Комната или корпус не найдены.</response>
		[HttpPut("Update/{id}")]
		public async Task<IActionResult> UpdateRoom(int id, EditRoom editroom)
		{
			var room = await _roomService.GetByIdAsync(id);
			if (room == null)
			{
				return NotFound();
			}

			var building = await _roomService.GetBuildingByIdAsync(editroom.BuildingId);
			if (building == null)
			{
				return NotFound($"Корпус с Id = {editroom.BuildingId} не существует.");
			}

			room.Name = editroom.Name;
			room.RoomTypeId = editroom.RoomTypeId;
			room.BuildingId = editroom.BuildingId;
			room.Capacity = editroom.Capacity;
			room.Floor = editroom.Floor;
			room.Number = editroom.Number;

			await _roomService.UpdateAsync(room);
			return NoContent();
		}

		/// <summary>
		/// Удаляет комнату по идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор комнаты.</param>
		/// <returns>Статус операции.</returns>
		/// <response code="204">Комната успешно удалена.</response>
		/// <response code="404">Комната не найдена.</response>
		[HttpDelete("Delete/{id}")]
		public async Task<IActionResult> DeleteRoom(int id)
		{
			var room = await _roomService.GetByIdAsync(id);
			if (room == null)
			{
				return NotFound();
			}

			await _roomService.DeleteAsync(room);
			return NoContent();
		}
	}
}