using Core.Dtos;
using Core.Entities;
using Infrastructure.Extentions.Base;

namespace Infrastructure.Repositories.LocationsR
{
    public interface ILocationsRepository : IBaseRepository<Locations>
    {
        public Task<List<Locations>> SearchAndPaging(LocationSearchDto dto);
    }
}
