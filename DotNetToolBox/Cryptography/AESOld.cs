using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DotNetToolBox.Cryptography
{
    public static class AESOld
    {
        public const int KEY_SIZE = 32;
        public const int IV_SIZE = 16;

        /// <summary>
        /// Encrypt data with the AES algorithm
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        /// <param name="key">Key</param>
        /// <param name="iv">IV</param>
        /// <param name="cipherMode">Cipher mode</param>
        /// <param name="paddingMode">Padding mode</param>
        /// <param name="bufferSize">Buffer size</param>
        /// <param name="notifyProgression">Notify progression method</param>
        public static void Encrypt(Stream input, Stream output, byte[] key, byte[] iv, CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7, int bufferSize = 4096, Action<int> notifyProgression = null)
        {
            using (AesManaged aes = new AesManaged())
            {
                aes.Mode = cipherMode;
                aes.Padding = paddingMode;
                ICryptoTransform cryptor = aes.CreateEncryptor(key, iv);
                using (CryptoStream cs = new CryptoStream(output, cryptor, CryptoStreamMode.Write))
                {
                    IO.StreamHelper.WriteStream(input, cs, bufferSize, notifyProgression);
                }
            }
        }

        /// <summary>
        /// Decrypt data with the AES algorithm
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        /// <param name="key">Key</param>
        /// <param name="iv">IV</param>
        /// <param name="cipherMode">Cipher mode</param>
        /// <param name="paddingMode">Padding mode</param>
        /// <param name="bufferSize">Buffer size</param>
        /// <param name="notifyProgression">Notify progression method</param>
        public static void Decrypt(Stream input, Stream output, byte[] key, byte[] iv, CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7, int bufferSize = 4096, Action<int> notifyProgression = null)
        {
            using (AesManaged aes = new AesManaged())
            {
                aes.Mode = cipherMode;
                aes.Padding = paddingMode;
                ICryptoTransform cryptor = aes.CreateDecryptor(key, iv);
                using (CryptoStream cs = new CryptoStream(output, cryptor, CryptoStreamMode.Write))
                {
                    IO.StreamHelper.WriteStream(input, cs, bufferSize, notifyProgression);
                }
            }
        }
    }
}
