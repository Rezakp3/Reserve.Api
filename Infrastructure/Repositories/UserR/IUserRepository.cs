using Core.Entities;
using Infrastructure.Extentions.Base;

namespace Infrastructure.Repositories.UserR
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<User> GetUserByUserName(string userName);
        public Task<bool> UserNameIsValidForInsert(string userName);
        public Task<bool> UserNameIsValidForUpdate(Guid id, string userName);
        public Task<bool> ChangePassword(Guid id, string passwordHash);
        public Task<bool> ChangeActivity(Guid id, bool activity);
        public Task<Guid> GetIdByUserName(string userName);
        public bool GetUserActive(Guid id);
    }
}
