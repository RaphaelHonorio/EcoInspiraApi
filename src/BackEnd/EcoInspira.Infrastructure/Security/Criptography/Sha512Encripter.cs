using EcoInspira.Domain.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace EcoInspira.Infrastructure.Security.Criptography
{
    public class Sha512Encripter : IPasswordEncripter
    {

        private readonly string _addtionalKey;

        public Sha512Encripter(string addtionalKey) => _addtionalKey = addtionalKey;


        public string Encrypt(string password)
        {

            var newPassword = $"{password}{_addtionalKey}";

            var bytes = Encoding.UTF8.GetBytes(password);
            var hasBytes = SHA512.HashData(bytes);

            return StringBytes(bytes);
        }

        private static string StringBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }
    }
}