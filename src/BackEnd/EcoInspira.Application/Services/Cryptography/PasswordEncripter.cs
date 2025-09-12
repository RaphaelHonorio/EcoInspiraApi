using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EcoInspira.Application.Services.Cryptography
{
    public class PasswordEncripter
    {

        private readonly string _addtionalKey;

        public PasswordEncripter(string addtionalKey) => _addtionalKey = addtionalKey;


        public string Encrypyt(string password)
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
