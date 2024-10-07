using BuildingsApi.Interfaces;
using BuildingsApi.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using BuildingsApi.RabbitModels;
using System.Text.Json;

namespace BuildingsApi.Services
{
	public class BuildingService
	{
		private readonly RabbitMQSettings _rabbitMQSettings;
		private readonly IBuildingRepository _buildingRepository;

		public BuildingService(IOptions<RabbitMQSettings> rabbitMQSettings,IBuildingRepository buildingRepository)
		{
			_rabbitMQSettings = rabbitMQSettings.Value;
			_buildingRepository = buildingRepository;
		}

		public async Task<IEnumerable<Building>> GetAllBuildingsAsync()
		{
			return await _buildingRepository.GetAllAsync();
		}

		public async Task<Building> GetBuildingByIdAsync(int id)
		{
			return await _buildingRepository.GetByIdAsync(id);
		}

		public async Task<Building> CreateBuildingAsync(Building building)
		{
			return await _buildingRepository.CreateAsync(building);
		}

		public async Task UpdateBuildingAsync(Building building)
		{
			await _buildingRepository.UpdateAsync(building);
		}

		public async Task DeleteBuildingAsync(Building building)
		{
			await _buildingRepository.DeleteAsync(building);
		}

		public void SendBuildingMessage(Building building, string action)
		{
			// Создаем фабрику подключения к RabbitMQ
			var factory = new ConnectionFactory
			{
				HostName = _rabbitMQSettings.Host,
				Port = _rabbitMQSettings.Port,
				UserName = _rabbitMQSettings.UserName,
				Password = _rabbitMQSettings.Password
			};

			// Устанавливаем соединение и канал
			using var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();

			// Объявляем очередь, если она еще не создана
			channel.QueueDeclare(
				queue: _rabbitMQSettings.QueueName,
				durable: false,
				exclusive: false,
				autoDelete: false,
				arguments: null);

			// Создаем объект сообщения
			var buildingMq = new BuildingMq(building)
			{
				Action = action // передаем действие (например, "update" или "delete")
			};

			// Серилизуем сообщение в JSON
			var message = JsonSerializer.Serialize(buildingMq);
			var body = Encoding.UTF8.GetBytes(message);

			// Публикуем сообщение в очередь RabbitMQ
			channel.BasicPublish(
				exchange: "",
				routingKey: _rabbitMQSettings.QueueName,
				basicProperties: null,
				body: body);

			Console.WriteLine($" [x] Sent {message}");
		}
	}
}