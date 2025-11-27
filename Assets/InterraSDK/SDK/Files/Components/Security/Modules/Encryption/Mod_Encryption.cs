using System;
using System.Security.Cryptography;
using System.Text;

namespace InTerra.FilesSDK
{
    public static partial class Comp_Security
    {
        public static partial class Mod_Encryption
        {
            #region APIs
            public static string Func_EncryptString(string plainText)
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(Static_EncryptionAttributes.key);
                    aes.IV = Encoding.UTF8.GetBytes(Static_EncryptionAttributes.iv);

                    ICryptoTransform encryptor = aes.CreateEncryptor();
                    byte[] buffer = Encoding.UTF8.GetBytes(plainText);
                    byte[] encrypted = encryptor.TransformFinalBlock(buffer, 0, buffer.Length);
                    return Convert.ToBase64String(encrypted);
                }
            }

            public static string Func_DecryptString(string encryptedText)
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(Static_EncryptionAttributes.key);
                    aes.IV = Encoding.UTF8.GetBytes(Static_EncryptionAttributes.iv);

                    ICryptoTransform decryptor = aes.CreateDecryptor();
                    byte[] buffer = Convert.FromBase64String(encryptedText);
                    byte[] decrypted = decryptor.TransformFinalBlock(buffer, 0, buffer.Length);
                    return Encoding.UTF8.GetString(decrypted);
                }
            }

            public static void Func_SetKeyAndIv(string newKey, string newIv)
            {
                Static_EncryptionAttributes.key = newKey;
                Static_EncryptionAttributes.iv = newIv;
            }
        }
        #endregion
    }
}