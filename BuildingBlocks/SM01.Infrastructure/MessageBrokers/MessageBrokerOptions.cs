using SM01.Infrastructure.MessageBrokers.Kafka;
using SM01.Infrastructure.MessageBrokers.RabbitMQ;

namespace SM01.Infrastructure.MessageBrokers
{
    public class MessageBrokerOptions
    {
        public string Provider { get; set; }

        public RabbitMQOptions RabbitMQ { get; set; }

        public KafkaOptions Kafka { get; set; }

        public bool UsedRabbitMQ()
        {
            return Provider == "RabbitMQ";
        }

        public bool UsedKafka()
        {
            return Provider == "Kafka";
        }

        public bool UsedFake()
        {
            return Provider == "Fake";
        }
    }
}
