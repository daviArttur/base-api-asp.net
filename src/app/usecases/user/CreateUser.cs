using Testes.src.app.contracts.usecases;
using Testes.src.app.interfaces;
using Testes.src.domain.entities;

namespace Testes.src.app.usecases.user
{
  public class CreateUserUseCase(IUserRepository _repository, IHashService _hashService) : ICreateUserUseCase
  {
    private readonly IUserRepository _repository = _repository;

    private readonly IHashService _hashService = _hashService;

    public async Task Perform(string email, string password)
    {
      User? user = await _repository.FindOneByEmail(email);
      if (user != null) throw new UserEmailAlreadyUsedException();
      string hashedPassword = this._hashService.Hash(password);
      user = User.Create(email, hashedPassword);
      await this._repository.Save(user);
    }
  }
}
