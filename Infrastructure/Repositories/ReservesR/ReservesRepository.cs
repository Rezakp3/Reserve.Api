using Core.Entities;
using Dapper;
using Dapper.FastCrud;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Infrastructure.Repositories.ReservesR
{
    public class ReservesRepository : IReservesRepository
    {
        private readonly string connectionString;
        public ReservesRepository(IConfiguration configuration)
            => connectionString = configuration.GetConnectionString("ReserveDb");

        public async Task<bool> Delete(Guid id)
        {
            using (var db = new SqlConnection(connectionString))
            {
                try
                {
                    return await db.ExecuteAsync("delete Reserves where Id = @Id", new { Id = id }) > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<List<Reserves>> GetAll()
        {
            using (var db = new SqlConnection(connectionString))
            {
                var res = await db.QueryAsync<Reserves>("select * from Reserves");
                return res.ToList();
            }
        }

        public async Task<Reserves> GetById(Guid id)
        {
            using (var db = new SqlConnection(connectionString))
            {
                var res = await db.QueryFirstOrDefaultAsync<Reserves>("select * from Reserves where Id = @Id", new { Id = id });
                return res;
            }
        }

        public async Task<bool> Insert(Reserves entity)
        {
            using (var db = new SqlConnection(connectionString))
            {
                try
                {
                    entity.CreateAt = DateTime.Now;
                    return await db.ExecuteAsync("insert into Reserves (CreateAt, ReserveDate, ReserverId, LocationId, Price) values (@CreateAt, @ReserveDate, @ReserverId, @LocationId, @Price)", entity) > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> Update(Reserves entity)
        {
            using (var db = new SqlConnection(connectionString))
            {
                try
                {
                    return await db.ExecuteAsync("update Reserves set ReserveDate = @ReserveDate, LocationId = @LocationId, Price = @Price where Id = @Id", entity) > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> ValidForInsert(DateTime reserveDate, Guid locationId)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var res = await con.ExecuteScalarAsync<int>
                    ("select count(Id) from Reserves where ReserveDate = @ReserveDate and LocationId = @LocationId",
                    new { ReserveDate = reserveDate, LocationId = locationId });

                return res == 0;
            }
        }

        public async Task<bool> ValidForUpdate(Guid id, DateTime reserveDate, Guid locationId)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var res = await con.ExecuteScalarAsync<int>
                    ("select count(Id) from Reserves where ReserveDate=@ReserveDate and LocationId = @LocationId and Id <> @Id",
                    new { ReserveDate = reserveDate, LocationId = locationId, Id = id });

                return res == 0;
            }
        }
    }
}
