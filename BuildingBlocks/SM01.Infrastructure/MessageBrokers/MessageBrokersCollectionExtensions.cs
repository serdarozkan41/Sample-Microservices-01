using SM01.Domain.Infrastructure.MessageBrokers;
using SM01.Infrastructure.MessageBrokers;
using SM01.Infrastructure.MessageBrokers.Fake;
using SM01.Infrastructure.MessageBrokers.Kafka;
using SM01.Infrastructure.MessageBrokers.RabbitMQ;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MessageBrokersCollectionExtensions
    {
        public static IServiceCollection AddFakeSender<T>(this IServiceCollection services)
        {
            services.AddSingleton<IMessageSender<T>>(new FakeSender<T>());
            return services;
        }

        public static IServiceCollection AddFakeReceiver<T>(this IServiceCollection services)
        {
            services.AddTransient<IMessageReceiver<T>>(x => new FakeReceiver<T>());
            return services;
        }

        public static IServiceCollection AddKafkaSender<T>(this IServiceCollection services, KafkaOptions options)
        {
            services.AddSingleton<IMessageSender<T>>(new KafkaSender<T>(options.BootstrapServers, options.Topics[typeof(T).Name]));
            return services;
        }

        public static IServiceCollection AddKafkaReceiver<T>(this IServiceCollection services, KafkaOptions options)
        {
            services.AddTransient<IMessageReceiver<T>>(x => new KafkaReceiver<T>(options.BootstrapServers,
                options.Topics[typeof(T).Name],
                options.GroupId));
            return services;
        }

        public static IServiceCollection AddRabbitMQSender<T>(this IServiceCollection services, RabbitMQOptions options)
        {
            services.AddSingleton<IMessageSender<T>>(new RabbitMQSender<T>(new RabbitMQSenderOptions
            {
                HostName = options.HostName,
                UserName = options.UserName,
                Password = options.Password,
                ExchangeName = options.ExchangeName,
                RoutingKey = options.RoutingKeys[typeof(T).Name],
            }));
            return services;
        }

        public static IServiceCollection AddRabbitMQReceiver<T>(this IServiceCollection services, RabbitMQOptions options)
        {
            services.AddTransient<IMessageReceiver<T>>(x => new RabbitMQReceiver<T>(new RabbitMQReceiverOptions
            {
                HostName = options.HostName,
                UserName = options.UserName,
                Password = options.Password,
                ExchangeName = options.ExchangeName,
                RoutingKey = options.RoutingKeys[typeof(T).Name],
                QueueName = options.QueueNames[typeof(T).Name],
                AutomaticCreateEnabled = true,
            }));
            return services;
        }

        public static IServiceCollection AddMessageBusSender<T>(this IServiceCollection services, MessageBrokerOptions options, HashSet<string> checkDulicated = null)
        {
            if (options.UsedRabbitMQ())
            {
                services.AddRabbitMQSender<T>(options.RabbitMQ);
            }
            else if (options.UsedKafka())
            {
                services.AddKafkaSender<T>(options.Kafka);
            }
            else if (options.UsedFake())
            {
                services.AddFakeSender<T>();
            }

            return services;
        }

        public static IServiceCollection AddMessageBusReceiver<T>(this IServiceCollection services, MessageBrokerOptions options)
        {
            if (options.UsedRabbitMQ())
            {
                services.AddRabbitMQReceiver<T>(options.RabbitMQ);
            }
            else if (options.UsedKafka())
            {
                services.AddKafkaReceiver<T>(options.Kafka);
            }
            else if (options.UsedFake())
            {
                services.AddFakeReceiver<T>();
            }

            return services;
        }
    }
}
