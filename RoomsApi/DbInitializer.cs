using RoomsApi.Models;

namespace RoomsApi
{
	public class DbInitializer
	{
		public static void Initialize(AppDbContext context)
		{
			// Создание базы данных, если её нет
			context.Database.EnsureCreated();

			// Проверка наличия данных
			if (context.RoomTypes.Any())
			{
				return; // База данных уже содержит начальные данные
			}

			// Добавление начальных данных для комнат
			var roomtypes = new[]
			{
				new RoomType { Name = "Лекционное" },
				new RoomType { Name = "Для працтических занятий" },
				new RoomType { Name = "Спортзал" }
			};
			context.RoomTypes.AddRange(roomtypes);


			// Сохранение изменений
			context.SaveChanges();
		}
	}
}
