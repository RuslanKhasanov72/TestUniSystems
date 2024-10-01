using BuildingsApi.Models;

namespace BuildingsApi.RabbitModels
{
	public class BuildingMq:Building
	{
		public string Action { get; set; }
		public BuildingMq(Building building)
		{
			this.Id = building.Id;
			this.Name = building.Name; 
			this.Address = building.Address;
			this.NumberOfFloors = building.NumberOfFloors;
												 
		}
	}
}
