using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace OrderProcessingService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Order Processing Service started...");

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            // Kuyruğu dinle
            channel.QueueDeclare(queue: "orders",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var order = JsonSerializer.Deserialize<Order>(message);

                // Siparişi işleme simülasyonu
                Console.WriteLine($"Order received: {order.OrderId}, Product: {order.ProductName}, Quantity: {order.Quantity}, Price: {order.Price}");

                // Burada sipariş onaylama veya işleme gibi bir işlem yapılabilir
            };

            // Kuyruktan mesajları tüketmeye başla
            channel.BasicConsume(queue: "orders",
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine("Listening for orders...");
            Console.ReadLine();
        }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
