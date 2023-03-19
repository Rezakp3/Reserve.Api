using Core.Dtos;
using Core.Entities;
using Dapper;
using Dapper.FastCrud;
using Infrastructure.Extentions.Base;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Repositories.LocationsR
{
    public class LocationsRepository : ILocationsRepository
    {
        private readonly string connectionString;
        public LocationsRepository(IConfiguration configuration)
            => connectionString = configuration.GetConnectionString("ReserveDb");

        public async Task<bool> Delete(Guid id)
        {
            using (var db = new SqlConnection(connectionString))
            {
                try
                {
                    return await db.ExecuteAsync("delete Locations where Id = @Id", new { Id = id }) > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<List<Locations>> GetAll()
        {
            using (var db = new SqlConnection(connectionString))
            {
                var res = await db.QueryAsync<Locations>("Select * from Locations");
                return res.ToList();
            }
        }

        public async Task<Locations> GetById(Guid id)
        {
            using (var db = new SqlConnection(connectionString))
            {
                var res = await db.QueryFirstOrDefaultAsync<Locations>("select * from Locations where Id = @Id", new { Id = id });
                return res;
            }
        }

        public async Task<bool> Insert(Locations entity)
        {
            using (var db = new SqlConnection(connectionString))
            {
                try
                {
                    entity.CreateAt = DateTime.Now;
                    return await db.ExecuteAsync("insert into Locations (LocationType, Title, Adres, CreateAt, CreatorId, Longitude, Latitude) values (@LocationType, @Title, @Adres, @CreateAt, @CreatorId, @Longitude, @Latitude)", entity) > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<List<Locations>> SearchAndPaging(LocationSearchDto dto)
        {
            using (var db = new SqlConnection(connectionString))
            {
                var res = await db.QueryAsync<Locations>("SearchAndPaging", dto, commandType: CommandType.StoredProcedure);
                return res.ToList();
            }
        }

        public async Task<bool> Update(Locations entity)
        {
            using (var db = new SqlConnection(connectionString))
            {
                try
                {
                    return await db.ExecuteAsync("Update Locations set Title = @Title,LocationType = @LocationType,Adres = @Adres,Longitude = @Longitude,Latitude = @Latitude where Id = @Id", entity) > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

    }
}
