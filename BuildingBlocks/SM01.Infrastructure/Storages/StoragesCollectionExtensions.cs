using Microsoft.Extensions.DependencyInjection;
using SM01.Infrastructure.Storages.Fake;
using SM01.Infrastructure.Storages.Local;

namespace SM01.Infrastructure.Storages
{
    public static class StoragesCollectionExtensions
    {
        public static IServiceCollection AddLocalStorageManager(this IServiceCollection services, LocalOption options)
        {
            services.AddSingleton<IFileStorageManager>(new LocalFileStorageManager(options));

            return services;
        }

        public static IServiceCollection AddFakeStorageManager(this IServiceCollection services)
        {
            services.AddSingleton<IFileStorageManager>(new FakeStorageManager());

            return services;
        }

        public static IServiceCollection AddStorageManager(this IServiceCollection services, StorageOptions options)
        {

            if (options.UsedLocal())
            {
                services.AddLocalStorageManager(options.Local);
            }
            else
            {
                services.AddFakeStorageManager();
            }

            return services;
        }
    }
}
