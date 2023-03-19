using Infrastructure.Repositories.AuthR;
using Infrastructure.Repositories.LocationsR;
using Infrastructure.Repositories.ReservesR;
using Infrastructure.Repositories.UserR;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration conn)
        {
            services.AddTransient<IAuthRepository, AuthRepository>();
            services.AddTransient<ILocationsRepository, LocationsRepository>();
            services.AddTransient<IReservesRepository, ReservesRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();
            return services;
        }
    }
}
