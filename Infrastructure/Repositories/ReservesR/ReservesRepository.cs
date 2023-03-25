using Core.Entities;
using Dapper;
using Dapper.FastCrud;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Repositories.ReservesR
{
    public class ReservesRepository : IReservesRepository
    {
        private readonly IDbConnection db;
        public ReservesRepository(IDbConnection db)
            => this.db = db;

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                await db.DeleteAsync(new Reserves { Id = id });
                return true;
                //return await db.ExecuteAsync("delete Reserves where Id = @Id", new { Id = id }) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Reserves>> GetAll()
        {
            var res = await db.QueryAsync<Reserves>("select * from Reserves");
            return res.ToList();
        }

        public async Task<Reserves> GetById(Guid id)
        {
            var res = await db.QueryFirstOrDefaultAsync<Reserves>("select * from Reserves where Id = @Id", new { Id = id });
            return res;
        }

        public async Task<bool> Insert(Reserves entity)
        {
            try
            {
                entity.CreateAt = DateTime.Now.Date;
                entity.ReserveDate = entity.ReserveDate.Date;
                await db.InsertAsync(entity);
                return true;
                //return await db.ExecuteAsync("insert into Reserves (CreateAt, ReserveDate, ReserverId, LocationId, Price) values (@CreateAt, @ReserveDate, @ReserverId, @LocationId, @Price)", entity) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(Reserves entity)
        {
            try
            {
                entity.ReserveDate = entity.ReserveDate.Date;
                await db.UpdateAsync(entity);
                return true;
                //return await db.ExecuteAsync("update Reserves set ReserveDate = @ReserveDate, LocationId = @LocationId, Price = @Price where Id = @Id", entity) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ValidForInsert(DateTime reserveDate, Guid locationId)
        {
            var res = await db.ExecuteScalarAsync<int>
                ("select count(Id) from Reserves where ReserveDate = @ReserveDate and LocationId = @LocationId",
                new { ReserveDate = reserveDate.Date, LocationId = locationId });

            return res == 0;
        }

        public async Task<bool> ValidForUpdate(Guid id, DateTime reserveDate, Guid locationId)
        {
            var res = await db.ExecuteScalarAsync<int>
                ("select count(Id) from Reserves where ReserveDate=@ReserveDate and LocationId = @LocationId and Id <> @Id",
                new { ReserveDate = reserveDate.Date, LocationId = locationId, Id = id });

            return res == 0;
        }
    }
}
