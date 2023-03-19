using Core.Entities;
using Dapper;
using Dapper.FastCrud;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Infrastructure.Repositories.UserR
{
    public class UserRepository : IUserRepository
    {
        private readonly string connectionString;
        public UserRepository(IConfiguration configuration)
            => connectionString = configuration.GetConnectionString("ReserveDb");

        public async Task<bool> ChangeActivity(Guid id, bool activity)
        {
            using (var db = new SqlConnection(connectionString))
            {
                try
                {
                    await db.ExecuteAsync("update [User] set IsActive = @activity where Id = @id", new { id, activity });
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> ChangePassword(Guid id, string passwordHash)
        {
            using (var db = new SqlConnection(connectionString))
            {
                try
                {
                    await db.ExecuteAsync("update [User] set PasswordHash = @passwordHash where Id = @id" , new {id , passwordHash});
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            using (var db = new SqlConnection(connectionString))
            {
                try
                {
                    return await db.ExecuteAsync("Delete [User] where Id = @Id", new { Id = id }) > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<List<User>> GetAll()
        {
            using (var db = new SqlConnection(connectionString))
            {
                var res = await db.QueryAsync<User>("Select * from [User]");
                return res.ToList();
            }
        }

        public async Task<User> GetById(Guid id)
        {
            using (var db = new SqlConnection(connectionString))
            {
                var res = await db.QueryFirstOrDefaultAsync<User>("Select * from [User] where Id = @Id", new { Id = id });
                return res;
            }
        }

        public async Task<Guid> GetIdByUserName(string userName)
        {
            using (var db = new SqlConnection(connectionString))
            {
                var res = await db.QueryFirstOrDefaultAsync<Guid>("Select Id from [User] where UserName = @UserName", new { UserName = userName });
                return res;
            }
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            using (var db = new SqlConnection(connectionString))
            {
                var res = await db.QueryFirstOrDefaultAsync<User>("select * from [User] where UserName = @userName" , new { userName });
                return res;
            }
        }

        public async Task<bool> Insert(User entity)
        {
            using (var db = new SqlConnection(connectionString))
            {
                try
                {
                    entity.CreateAt = DateTime.Now;
                    return await db.ExecuteAsync("Insert into [User] (UserName, PasswordHash, IsActive, CreateAt, FName, LName) values (@UserName, @PasswordHash, @IsActive, @CreateAt, @FName, @LName)", entity) > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> Update(User entity)
        {
            using (var db = new SqlConnection(connectionString))
            {
                try
                {
                    return await db.ExecuteAsync("UPDATE [User] SET UserName = @UserName, FName = @FName, LName = @LName") > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> UserNameIsValidForInsert(string userName)
        {
            using (var db = new SqlConnection(connectionString))
            {
                var res = await db.QueryFirstOrDefaultAsync<User>("Select * from [User] where UserName = @UserName", new { UserName = userName });
                return res is null;
            }
        }

        public async Task<bool> UserNameIsValidForUpdate(Guid id , string userName)
        {
            using (var db = new SqlConnection(connectionString))
            {
                var res = await db.QueryFirstOrDefaultAsync<User>("select * from [User] where Id <> @id and UserName = @userName", new { userName, id });
                return res is null;
            }
        }
    }
}
