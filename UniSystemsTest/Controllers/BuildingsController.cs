using BuildingsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuildingsApi.EditModels;
using System.Numerics;
using System.Net;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;

namespace BuildingsApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BuildingsController : ControllerBase
	{
		private readonly AppDbContext _appDbContext;
		private readonly BuildingService _buildingService;

		public BuildingsController(AppDbContext appDbContext, BuildingService buildingService)
		{
			_buildingService = buildingService;
			_appDbContext = appDbContext;
		}
		// Get all buildings
		[HttpGet]
		[Route("GetAll")]
		public async Task<ActionResult<IEnumerable<Building>>> GetBuildings()
		{
			return await _appDbContext.Buildings.ToListAsync();
		}

		// Get building by id
		[HttpGet]
		[Route("GetBy/{id:int}")]
		public async Task<ActionResult<Building>> GetBuilding(int id)
		{
			var building = await _appDbContext.Buildings.FindAsync(id);

			if (building == null)
			{
				return NotFound();
			}

			return building;
		}

		// Create new building
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
			_appDbContext.Buildings.Add(building);
			await _appDbContext.SaveChangesAsync();

			_buildingService.SendBuildingMessage(building,"update");

			return CreatedAtAction(nameof(GetBuilding), new { id = building.Id }, building);
		}

		// Update existing building
		[HttpPut("Update/{id}")]
		public async Task<IActionResult> UpdateBuilding(int id, EditBuilding editbuilding)
		{
			var building = await _appDbContext.Buildings.FindAsync(id);

			if (building==null)
			{
				return NotFound();
			}

			building.Name = editbuilding.Name;
			building.Address = editbuilding.Address;
			building.NumberOfFloors = editbuilding.NumberOfFloors;

			await _appDbContext.SaveChangesAsync();

			_buildingService.SendBuildingMessage(building,"update");


			return Ok();
		}

		// Delete building
		[HttpDelete("Delete{id}")]
		public async Task<IActionResult> DeleteBuilding(int id)
		{
			var building = await _appDbContext.Buildings.FindAsync(id);
			if (building == null)
			{
				return NotFound();
			}

			_appDbContext.Buildings.Remove(building);

			_buildingService.SendBuildingMessage(building,"delete");

			await _appDbContext.SaveChangesAsync();

			return NoContent();
		}
	}
}