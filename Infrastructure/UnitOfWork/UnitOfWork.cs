using Infrastructure.Repositories.AuthR;
using Infrastructure.Repositories.LocationsR;
using Infrastructure.Repositories.ReservesR;
using Infrastructure.Repositories.UserR;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IAuthRepository auth,
            ILocationsRepository locations,
            IReservesRepository reserves,
            IUserRepository user)
        {
            Auth = auth;
            Locations = locations;
            Reserves = reserves;
            User = user;
        }

        public IAuthRepository Auth { get; }

        public ILocationsRepository Locations { get; }

        public IReservesRepository Reserves { get; }

        public IUserRepository User { get; }


    }
}
