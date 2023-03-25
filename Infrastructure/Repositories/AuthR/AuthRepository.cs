using Core.Entities;
using Dapper;
using Dapper.FastCrud;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Repositories.AuthR
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDbConnection db;
        public AuthRepository(IDbConnection connection)
        {
            db = connection;
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                await db.DeleteAsync(new Auth { Id = id });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Auth>> GetAll()
        {
            var res = await db.FindAsync<Auth>();
            return res.ToList();

        }

        public async Task<Auth> GetById(Guid id)
        {
            var res = await db.GetAsync(new Auth { Id = id });
            return res;

        }

        public async Task<Auth> GetByRefreshToken(string refreshToken)
        {
            var res = await db.QueryFirstOrDefaultAsync<Auth>("select * from Auth where RefreshToken = @refToken" , new {refToken = refreshToken});
            return res;
        }

        public async Task<bool> Insert(Auth entity)
        {
            try
            {
                await db.InsertAsync(entity);
                //await db.ExecuteAsync("insert into Auth (Id, AccessToken, RefreshToken, RefreshTokenExpirationDate) values (@Id, @AccessToken, @RefreshToken, @RefreshTokenExpirationDate)", entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(Auth entity)
        {
            try
            {
                await db.UpdateAsync(entity);
                //await db.ExecuteAsync("update Auth set AccessToken = @AccessToken, RefreshToken = @RefreshToken, RefreshTokenExpirationDate = @RefreshTokenExpirationDate", entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
