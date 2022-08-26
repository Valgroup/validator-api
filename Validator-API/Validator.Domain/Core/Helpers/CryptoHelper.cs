using System.Security.Cryptography;
using System.Text;

namespace Validator.Domain.Core.Helpers
{
    public class CryptoHelper
    {
        private const string Key = "b243ad862f664052bc16adf96a74fcb8";
        public static string Crypto(string value)
        {
            byte[] iv = { 55, 34, 87, 64, 87, 195, 54, 21 };
            byte[] encryptKey = Encoding.UTF8.GetBytes(Key.Substring(0, 8));
            var des = new DESCryptoServiceProvider();
            var inputByte = Encoding.UTF8.GetBytes(value);
            var mStream = new MemoryStream();
            var cStream = new CryptoStream(mStream, des.CreateEncryptor(encryptKey, iv), CryptoStreamMode.Write);
            cStream.Write(inputByte, 0, inputByte.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }

        public static string Decrypt(string valueEncrypted)
        {
            byte[] iv = { 55, 34, 87, 64, 87, 195, 54, 21 };
            byte[] decryptKey = Encoding.UTF8.GetBytes(Key.Substring(0, 8));
            var des = new DESCryptoServiceProvider();
            var inputByte = Convert.FromBase64String(valueEncrypted);
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, des.CreateDecryptor(decryptKey, iv), CryptoStreamMode.Write);
            cs.Write(inputByte, 0, inputByte.Length);
            cs.FlushFinalBlock();
            var encoding = Encoding.UTF8;

            return encoding.GetString(ms.ToArray());
        }
    }
}
