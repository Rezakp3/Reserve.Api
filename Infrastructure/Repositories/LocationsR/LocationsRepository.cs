using Core.Dtos;
using Core.Entities;
using Dapper;
using Dapper.FastCrud;
using System.Data;

namespace Infrastructure.Repositories.LocationsR
{
    public class LocationsRepository : ILocationsRepository
    {
        private readonly IDbConnection db;

        public LocationsRepository(IDbConnection dbConnection)
            => db = dbConnection;


        public async Task<bool> Delete(Guid id)
        {
            try
            {
                await db.DeleteAsync(new Locations() { Id = id });
                //return await db.ExecuteAsync("delete Locations where Id = @Id", new { Id = id }) > 0;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Locations>> GetAll()
        {
            var res = await db.QueryAsync<Locations>("Select * from Locations");
            return res.ToList();
        }

        public async Task<Locations> GetById(Guid id)
        {
            var res = await db.QueryFirstOrDefaultAsync<Locations>("select * from Locations where Id = @Id", new { Id = id });
            return res;
        }

        public async Task<bool> Insert(Locations entity)
        {
            try
            {
                entity.CreateAt = DateTime.Now.Date;
                await db.InsertAsync(entity);
                return true;
                //return await db.ExecuteAsync("insert into Locations (LocationType, Title, Adres, CreateAt, CreatorId, Longitude, Latitude) values (@LocationType, @Title, @Adres, @CreateAt, @CreatorId, @Longitude, @Latitude)", entity) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Locations>> SearchAndPaging(LocationSearchDto dto)
        {
            var res = db.Query<Locations>("SearchAndPaging", dto, commandType: CommandType.StoredProcedure).ToList();
            return res;
        }

        public async Task<bool> Update(Locations entity)
        {
            try
            {
                await db.UpdateAsync(entity);
                return true;
                //return await db.ExecuteAsync("Update Locations set Title = @Title,LocationType = @LocationType,Adres = @Adres,Longitude = @Longitude,Latitude = @Latitude where Id = @Id", entity) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
