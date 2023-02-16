using Bookstore.API.Helpers;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Bookstore.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IBookTransactionsService, BookTransactionsService>();

            services.AddScoped(typeof(IRepository<>), (typeof(Repository<>)));

            services.AddAutoMapper(typeof(MappingProfiles));

            return services;
        }
    }
}
