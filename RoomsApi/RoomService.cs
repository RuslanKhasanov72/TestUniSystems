using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RoomsApi.RabbitModels;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RoomsApi.Models;
using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;

public class RoomService:BackgroundService
{
	readonly IServiceScopeFactory _factory;
	private IModel _channel;
	private IConnection _connection;
	public RoomService(IServiceScopeFactory factory)
	{
		_factory = factory;
	}

	public override Task StartAsync(CancellationToken cancellationToken)
	{
		var factory = new ConnectionFactory() {
			HostName = "rabbitmq", // или IP-адрес RabbitMQ
			Port = 5672, // Порт по умолчанию для RabbitMQ
			UserName = "guest", // Ваше имя пользователя
			Password = "guest", // Ваш пароль
		 DispatchConsumersAsync = true };
		_connection = factory.CreateConnection();
		_channel = _connection.CreateModel();

		// Подписка на очередь
		_channel.QueueDeclare(queue: "building_changes",
							 durable: false,
							 exclusive: false,
							 autoDelete: false,
							 arguments: null);

		return base.StartAsync(cancellationToken);
	}
	protected override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		

		var consumer = new AsyncEventingBasicConsumer(_channel);
		consumer.Received += async (model, ea) =>
		{
			var body = ea.Body.ToArray();
			var message = Encoding.UTF8.GetString(body);


			var building = JsonSerializer.Deserialize<BuildingMq>(message);

			if (building.Action == "update")
			{
				await UpdateBuildingInRoomService(building);
			}
			else
			{
				await DeleteBuildingInRoomService(building);
			}

			Console.WriteLine($" [x] Received {message}");
		};

		_channel.BasicConsume(queue: "building_changes",
							 autoAck: true,
							 consumer: consumer);

		return Task.CompletedTask;
	}

	public override async Task StopAsync(CancellationToken cancellationToken)
	{
		await base.StopAsync(cancellationToken);
		_connection.Close();
	}

	private async Task UpdateBuildingInRoomService(Building building)
	{
		using var scope = _factory.CreateScope();
		var dbContext =  scope.ServiceProvider.GetRequiredService<AppDbContext>();

		var buildingold = await dbContext.Buildings.FindAsync(building.Id);

		if (buildingold == null)
		{
			dbContext.Buildings.Add(building);
			await dbContext.SaveChangesAsync();
		}
		else
		{
			buildingold.Name = building.Name;
			buildingold.NumberOfFloors = building.NumberOfFloors;
			await dbContext.SaveChangesAsync();
		}
	}

	private async Task DeleteBuildingInRoomService(Building building)
	{
		using var scope = _factory.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

		var buildingold = await dbContext.Buildings.FindAsync(building.Id);
		if (buildingold != null)
		{
			var roomsWithBuildId = await dbContext.Rooms.Where(r => r.BuildingId == building.Id).ToListAsync();

			dbContext.Rooms.RemoveRange(roomsWithBuildId);
			dbContext.Buildings.Remove(buildingold);
			await dbContext.SaveChangesAsync();
		}

	}
}