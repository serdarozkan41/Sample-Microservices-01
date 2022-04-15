using SM01.Domain.Infrastructure.MessageBrokers;

namespace SM01.Infrastructure.MessageBrokers.Fake
{
    public class FakeSender<T> : IMessageSender<T>
    {
        public Task SendAsync(T message, MetaData metaData = null, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
