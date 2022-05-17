using System;
using System.Text;

namespace iTrainee.Services.Security
{
    public static class EncryptAndDecrypt
    {
        public static string Key = "sikj#@#&*&fijfd";

        public static string ConvertToEncrypt(string encryptString)
        {
            if (string.IsNullOrEmpty(encryptString)) return "";
            encryptString += Key;
            var passwordBytes = Encoding.UTF8.GetBytes(encryptString);

            return Convert.ToBase64String(passwordBytes);
        }

        public static string ConvertToDecrypt(string decryptString)
        {
            if (string.IsNullOrEmpty(decryptString)) return "";
            var encodeBytes = Convert.FromBase64String(decryptString);
            var result = Encoding.UTF8.GetString(encodeBytes);
 
            return result.Substring(0, result.Length - Key.Length);
        }
    }
}
