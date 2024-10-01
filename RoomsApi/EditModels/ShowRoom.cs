namespace RoomsApi.EditModels
{
	public class ShowRoom
	{
		public int Id { get; set; }
		public string BuildingName { get; set; }
		public string Name { get; set; }
		public string RoomTypeName { get; set; }
		public int Capacity { get; set; }
		public int Floor { get; set; }
		public int Number { get; set; }
	}
}
