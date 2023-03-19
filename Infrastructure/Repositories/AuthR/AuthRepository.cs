using Core.Entities;
using Core.Extentions;
using Dapper;
using Dapper.FastCrud;
using Infrastructure.Extentions.Base;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Infrastructure.Repositories.AuthR
{
    public class AuthRepository : IAuthRepository
    {
        private readonly string connectionString;
        public AuthRepository(IConfiguration configuration)
            => connectionString = configuration.GetConnectionString("ReserveDb");

        public async Task<bool> Delete(Guid id)
        {
            using (var db = new SqlConnection(connectionString))
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
        }

        public async Task<List<Auth>> GetAll()
        {
            using (var db = new SqlConnection(connectionString))
            {
                var res = await db.FindAsync<Auth>();
                return res.ToList();
            }
        }

        public async Task<Auth> GetById(Guid id)
        {
            using (var db = new SqlConnection(connectionString))
            {
                var res = await db.GetAsync(new Auth { Id = id });
                return res;
            }
        }

        public async Task<bool> Insert(Auth entity)
        {
            using (var db = new SqlConnection(connectionString))
            {
                try
                {
                    await db.ExecuteAsync("insert into Auth (Id, AccessToken, RefreshToken, RefreshTokenExpirationDate) values (@Id, @AccessToken, @RefreshToken, @RefreshTokenExpirationDate)", entity);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> Update(Auth entity)
        {
            using (var db = new SqlConnection(connectionString))
            {
                try
                {
                    await db.ExecuteAsync("update Auth set AccessToken = @AccessToken, RefreshToken = @RefreshToken, RefreshTokenExpirationDate = @RefreshTokenExpirationDate", entity);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
