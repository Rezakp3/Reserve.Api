using Infrastructure.Repositories.LocationsR;
using Infrastructure.Repositories.ReservesR;
using Infrastructure.Repositories.UserR;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        public ILocationsRepository Locations { get; }
        public IReservesRepository Reserves { get; }
        public IUserRepository User { get; }
    }
}
