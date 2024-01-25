using Testes.src.domain.entities;

namespace Testes.src.app.interfaces
{
    public interface IUserRepository
    {
        public Task<User?> FindOneById(int id);
        public Task<User?> FindOneByEmail(string email);
        public Task Save(User user);
    }
}
