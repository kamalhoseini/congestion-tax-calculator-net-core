using Application.Common.Interfaces;
using Domain.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CongestionContext>(options =>
                options.UseInMemoryDatabase("Congestion"));


            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ITaxExemptVehicleRepository, TaxExemptVehicleRepository>();
            services.AddScoped<IExemptDateRepository, ExemptDateRepository>();
            services.AddScoped<ITaxAmountRepository, TaxAmountRepository>();
            services.AddScoped<ITaxCalculatorService, TaxCalculatorService>();

            return services;
        }
    }
}
