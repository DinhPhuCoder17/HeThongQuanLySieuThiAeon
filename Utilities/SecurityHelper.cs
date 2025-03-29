namespace Utilities
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class SecurityHelper
    {
        private static readonly string key = "YourSecretKey123"; // Key bí mật, cần bảo mật

        public static string Encrypt(string text)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] textBytes = Encoding.UTF8.GetBytes(text);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                {
                    byte[] encryptedBytes = encryptor.TransformFinalBlock(textBytes, 0, textBytes.Length);
                    return Convert.ToBase64String(encryptedBytes);
                }
            }
        }

        public static string Decrypt(string encryptedText)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                {
                    byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
        }
    }
}
