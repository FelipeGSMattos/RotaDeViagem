using RotaDeViagem.Application.Interface;
using RotaDeViagem.Application.Services;
using RotaDeViagem.Domain.Interfaces.Repositories;
using RotaDeViagem.Domain.Interfaces.Services;
using RotaDeViagem.Domain.Services;
using RotaDeViagem.Infra.Data.Repositories;

namespace RotaDeViagem.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IRotaAppService, RotaAppService>();
            services.AddScoped<IRotaService, RotaService>();
            services.AddScoped<IRotaRepository, RotaRepository>();

            return services;
        }
    }
}
