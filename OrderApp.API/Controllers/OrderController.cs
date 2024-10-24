using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            // RabbitMQ bağlantısı kur
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            // Kuyruğu oluştur
            channel.QueueDeclare(queue: "orders",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            // Siparişi JSON formatına dönüştür
            var orderJson = JsonSerializer.Serialize(order);
            var body = Encoding.UTF8.GetBytes(orderJson);

            // Mesajı RabbitMQ'ya gönder
            channel.BasicPublish(exchange: "",
                                 routingKey: "orders",
                                 basicProperties: null,
                                 body: body);

            return Ok("Order created and sent to queue.");
        }
    }

    // Sipariş Modeli
    public class Order
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
