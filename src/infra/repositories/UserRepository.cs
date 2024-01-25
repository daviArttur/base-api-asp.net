using Testes.src.infra.config.db;
using Testes.src.domain.entities;
using Testes.src.app.interfaces;
using Testes.src.infra.models;
using Testes.src.domain.dto;
using Microsoft.EntityFrameworkCore;

namespace Testes.src.infra.repositories
{
  public class UserRepository(AppDbContext context) : IUserRepository
  {
    private AppDbContext _context = context;
    public async Task Save(User user)
    {
      try
      {
        UserModel userModel = new UserModel
        {
          Email = user.Email,
          Password = user.Password,
          Tools = { },
        };
        await _context.Users.AddAsync(userModel);
        await _context.SaveChangesAsync();
      }
      catch
      {
        throw new QueryException();
      }
    }

    public async Task<User?> FindOneById(int id)
    {
      try
      {
        UserModel? userModel = await this._context.Users.Where(user => user.Id == id).FirstOrDefaultAsync();
        if (userModel == null)
        {
          return null;
        }
        User user = new User(new CreateUserDto { Email = userModel.Email, Id = userModel.Id, Password = userModel.Password });
        return user;
      }
      catch
      {
        throw new QueryException();
      }
    }

    public async Task<User?> FindOneByEmail(string email)
    {
      try
      {
        UserModel? userModel = await this._context.Users.Where(user => user.Email == email).FirstOrDefaultAsync();
        if (userModel == null)
        {
          return null;
        }
        User user = new User(new CreateUserDto { Email = userModel.Email, Id = userModel.Id, Password = userModel.Password });
        return user;
      }
      catch
      {
        throw new QueryException();
      }
    }
  }
}

