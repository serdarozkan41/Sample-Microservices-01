using SM01.Domain.Infrastructure.MessageBrokers;

namespace SM01.Infrastructure.MessageBrokers.Fake
{
    public class FakeReceiver<T> : IMessageReceiver<T>
    {
        public void Receive(Action<T, MetaData> action)
        {
            // do nothing
        }
    }
}
