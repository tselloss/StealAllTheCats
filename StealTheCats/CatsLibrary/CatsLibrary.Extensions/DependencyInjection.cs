using CatsLibrary.Controller;
using CatsLibrary.Interface;
using CatsLibrary.Services;
using DatabaseContext.DBHelper.Methods;
using Extensions.HttpClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CatsLibrary.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCatsLibraryServices(this IServiceCollection services)
        {
            services.TryAddScoped<ICatsInterface, CatsService>();
            services.TryAddScoped<CatsController>();
            services.TryAddScoped<HttpHelper>();
            services.TryAddScoped<DbHelper>();

            return services;
        }
    }
}
