using BuildingsApi.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BuildingsApi.RabbitModels;

public class BuildingService
{
		public void SendBuildingMessage(Building building,string action)
		{
			var factory = new ConnectionFactory() { HostName = "localhost" };
			using var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();

			// Объявление очереди, если ее еще нет
			channel.QueueDeclare(queue: "building_changes",
								 durable: false,
								 exclusive: false,
								 autoDelete: false,
								 arguments: null);
			BuildingMq buildingMq = new BuildingMq(building);
			buildingMq.Action = action;
			// Серилизация корпуса в JSON
			var message = JsonSerializer.Serialize(building);
			var body = Encoding.UTF8.GetBytes(message);

			// Отправка сообщения в RabbitMQ
			channel.BasicPublish(exchange: "",
								 routingKey: "building_changes",
								 basicProperties: null,
								 body: body);

			Console.WriteLine($" [x] Sent {message}");
		}
}

	