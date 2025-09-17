using EcoInspira.Domain.Security.Cryptography;
using EcoInspira.Infrastructure.Security.Criptography;

namespace CommonTestUtilities.Cryptography
{
    public class PasswordEncripterBuilder
    {
        public static IPasswordEncripter Build() => new Sha512Encripter("abbs");
    }
}
