using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomsApi.EditModels;
using RoomsApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using System.Xml.Linq;
namespace RoomsApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoomsController : ControllerBase
	{
		private readonly AppDbContext _appDbContext;

		public RoomsController(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}
		// Get all rooms
		[HttpGet]
		[Route("GetAll")]
		public async Task<ActionResult<IEnumerable<ShowRoom>>> GetRooms()
		{
			return await _appDbContext.Rooms.Select(room => new ShowRoom
			{
			Id=room.Id,
			 Name=room.Name,
			 BuildingName=_appDbContext.Buildings.FirstOrDefault(b=>b.Id==room.BuildingId).Name,
			 RoomTypeName= _appDbContext.RoomTypes.FirstOrDefault(b => b.Id == room.RoomTypeId).Name,
			 Capacity=room.Capacity,
			 Floor=room.Floor,
			 Number=room.Number
			}).ToListAsync();
		}

		// Get room by id
		[HttpGet]
		[Route("GetBy/{id:int}")]
		public async Task<ActionResult<Room>> GetRoom(int id)
		{
			var room = await _appDbContext.Rooms.FindAsync(id);

			if (room == null)
			{
				return NotFound();
			}

			return room;
		}
		// Create new room
		[HttpPost]
		[Route("Add")]
		public async Task<ActionResult<Room>> CreateRoom(EditRoom editroom)
		{
			var building = await _appDbContext.Buildings.FindAsync(editroom.BuildingId);

			if (building == null)
			{
				return NotFound($"Корпус с Id = {editroom.BuildingId} не существует.");
			}
			else
			{
				if (building.NumberOfFloors < editroom.Floor)
					return BadRequest($"Этажа номер {editroom.Floor} не существует в корпусе {building.Name}");
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
			_appDbContext.Rooms.Add(room);
			await _appDbContext.SaveChangesAsync();

			return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
		}

		// Update existing room
		[HttpPut("Update/{id}")]
		public async Task<IActionResult> UpdateRoom(int id, EditRoom editroom)
		{
			var room = await _appDbContext.Rooms.FindAsync(id);

			if (room == null)
			{
				return NotFound();
			}

			var building = await _appDbContext.Buildings.FindAsync(editroom.BuildingId);

			if (building==null)
			{
				return NotFound($"Корпус с Id = {editroom.BuildingId} не существует.");
			}
			else
			{
				if(building.NumberOfFloors<editroom.Floor)
				 return BadRequest($"Этажа номер {editroom.Floor} не существует в корпусе {building.Name}"); 
			}
			room.Name = editroom.Name;
			room.RoomTypeId = editroom.RoomTypeId;
			room.BuildingId = editroom.BuildingId;
			room.Capacity = editroom.Capacity;
			room.Floor = editroom.Floor;
			room.Number = editroom.Number;

			await _appDbContext.SaveChangesAsync();


			return Ok();
		}

		// Delete room
		[HttpDelete("Delete{id}")]
		public async Task<IActionResult> DeleteRoom(int id)
		{
			var room = await _appDbContext.Rooms.FindAsync(id);
			if (room == null)
			{
				return NotFound();
			}

			_appDbContext.Rooms.Remove(room);
			await _appDbContext.SaveChangesAsync();

			return NoContent();
		}
	}
}
