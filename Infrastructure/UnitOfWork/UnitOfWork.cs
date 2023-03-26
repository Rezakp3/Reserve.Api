using Infrastructure.Repositories.LocationsR;
using Infrastructure.Repositories.ReservesR;
using Infrastructure.Repositories.UserR;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            ILocationsRepository locations,
            IReservesRepository reserves,
            IUserRepository user)
        {
            Locations = locations;
            Reserves = reserves;
            User = user;
        }


        public ILocationsRepository Locations { get; }

        public IReservesRepository Reserves { get; }

        public IUserRepository User { get; }


    }
}
