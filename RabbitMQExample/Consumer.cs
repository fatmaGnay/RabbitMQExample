using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQExample
{
    public class Consumer
    {
        private readonly RabbitMQService _rabbitMQService;//Tanıtıldı
        public Consumer(string queueName)
        {
            _rabbitMQService = new RabbitMQService(); // Ram'de nesneyi tanıttı.
            using (var connection = _rabbitMQService.GetRabbitMQConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    // Received(alıcı) event'i sürekli listen modunda olacaktır.
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body.ToArray());
                        Console.WriteLine($"{queueName} isimli queue üzerinden gelen mesaj : \"{message}\" ");
                    };
                    channel.BasicConsume(queueName, true, consumer);
                    //noAck: True olarak set edildiği taktirde, consumer mesajı aldığı zaman otomatik olarak mesaj Queue’dan silinecektir.
                    //Eğer Queue üzerinden silinmesini istemiyor iseniz, False olarak set etmeniz gerekmektedir.
                    Console.ReadKey();
                }
            }
        }
    }
}
