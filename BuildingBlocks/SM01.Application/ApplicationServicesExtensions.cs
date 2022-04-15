using SM01.Application.Common.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped(typeof(ICrudService<>), typeof(CrudService<>));

            return services;
        }
    }
}
