using Core.Entities;
using Infrastructure.Extentions.Base;

namespace Infrastructure.Repositories.ReservesR
{
    public interface IReservesRepository : IBaseRepository<Reserves>
    {
        Task<bool> ValidForInsert(DateTime reserveDate, Guid locationId);
        Task<bool> ValidForUpdate(Guid id, DateTime reserveDate, Guid locationId);
    }
}
