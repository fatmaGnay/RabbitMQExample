using System;

namespace RabbitMQExample
{
    class Program
    {
        private static readonly string _queueName = "Fatma ";
        private static Publisher publisher;
        private static Consumer consumer;
        static void Main(string[] args)
        {
            publisher = new Publisher(_queueName, "Merhaba ");
            consumer = new Consumer(_queueName);
            Console.ReadKey();
        }
    }
}
