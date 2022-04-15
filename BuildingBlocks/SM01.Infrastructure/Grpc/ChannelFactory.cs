using Grpc.Net.Client;

namespace SM01.Infrastructure.Grpc
{
    public class ChannelFactory
    {
        public static GrpcChannel Create(string address)
        {
            var channel = GrpcChannel.ForAddress(address,
                new GrpcChannelOptions
                {
                    HttpClient = new HttpClient(new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                        {
                            // TODO: verify the Certificate
                            return true;
                        },
                    }),
                });

            return channel;
        }
    }
}
