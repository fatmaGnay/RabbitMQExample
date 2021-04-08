using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQExample
{
    /// <summary>
    /// Channel  =>  sayesinde bir Queue oluşturabilirken, mesaj gönderme işlemlerini de gerçekleştirebilmekteyiz.
    /// </summary>
    public class Publisher
    {
        private readonly RabbitMQService _rabbitMQService;
        public Publisher(string queueName, string message)//ctor
        {
            _rabbitMQService = new RabbitMQService();
            using (var connection = _rabbitMQService.GetRabbitMQConnection())
            {
                using (var channel = connection.CreateModel())//CreateModel() methodu yeni bir channel oluşturur.
                                                              //channel(session) ile yeni bir kuyruk oluşturulup istenen mesajı bu kanal üzerinden gönderirir. 
                {
                    channel.QueueDeclare(queueName, false, false, false, null);// QueueuDeclare() methodu kuyruk oluşturur.
                    channel.BasicPublish("", queueName, null, Encoding.UTF8.GetBytes(message));// BasicPublish Kuyrua mesajı gönderiyor ! UTF-8 olarak !
                    Console.WriteLine($"{queueName} queue'su üzerine ,\"{message}\" mesajı yazıldı.");
                }
            }

        }
    }
}
