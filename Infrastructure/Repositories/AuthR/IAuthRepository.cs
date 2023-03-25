using Core.Entities;
using Infrastructure.Extentions.Base;

namespace Infrastructure.Repositories.AuthR
{
    public interface IAuthRepository : IBaseRepository<Auth>
    {
        public Task<Auth> GetByRefreshToken(string refreshToken); 
    }
}
