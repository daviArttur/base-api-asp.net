using Microsoft.AspNetCore.Identity;
using Testes.src.app.interfaces;

namespace Testes.src.infra.services
{
    public class HashService() : IHashService
    {
        private readonly PasswordHasher<UserPassword> _passwordHasher = new PasswordHasher<UserPassword>();

        public string Hash(string rawPassword)
        {
            UserPassword user = new UserPassword();
            string hashedPassword = _passwordHasher.HashPassword(user, rawPassword);
            return hashedPassword;
        }

        public bool Compare(string rawPassword, string hashedPassword)
        {
            UserPassword user = new UserPassword();
            var result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, rawPassword);
            return result == PasswordVerificationResult.Success;
        }
    }

    public class UserPassword {}
}