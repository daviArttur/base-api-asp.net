using Testes.src.app.interfaces;
using Testes.src.domain.entities;
using Testes.src.app.contracts.usecases;

namespace Testes.src.app.usecases.user
{
  public class SignInUserUseCase(IUserRepository _repository, IHashService _hashService, IAuthService _AuthService) : ISignInUserUseCase
  {
    private readonly IUserRepository _repository = _repository;
    private readonly IHashService _hashService = _hashService;
    private readonly IAuthService _AuthService = _AuthService;

    public async Task<SignInUserOutput> Perform(string email, string rawPassword)
    {
      User? user = await _repository.FindOneByEmail(email);
      if(user == null) throw new UserNotFoundException();
      bool passwordAreEqual = this._hashService.Compare(rawPassword, user.Password);
      if(!passwordAreEqual) throw new UserNotFoundException();
      string accessToken = this._AuthService.GenerateToken(user.Id);
      return new SignInUserOutput{ AccessToken = accessToken };
    }
  }
}
