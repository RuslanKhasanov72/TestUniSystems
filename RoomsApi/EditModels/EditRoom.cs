using RoomsApi.Models;

namespace RoomsApi.EditModels
{
	public class EditRoom
	{
		public int BuildingId { get; set; }
		public string Name { get; set; }
		public int RoomTypeId { get; set; }
		public int Capacity { get; set; }
		public int Floor { get; set; }
		public int Number { get; set; }
	}
}
