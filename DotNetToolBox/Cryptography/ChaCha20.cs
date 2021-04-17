using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.IO;

namespace DotNetToolBox.Cryptography
{
    public static class ChaCha20
    {
        public const int KEY_SIZE = 32;
        public const int NONCE_SIZE = 8;

        /// <summary>
        /// Encrypt data with ChaCha20
        /// </summary>
        /// <param name="data">Data to encrypt</param>
        /// <param name="key">Key</param>
        /// <param name="nonce">Nonce</param>
        /// <returns>Encrypted data</returns>
        public static byte[] Encrypt(byte[] data, byte[] key, byte[] nonce)
        {
            ChaChaEngine engine = new ChaChaEngine();
            ParametersWithIV parameters = new ParametersWithIV(new KeyParameter(key, 0, key.Length), nonce, 0, nonce.Length);
            engine.Init(true, parameters);
            byte[] enc = new byte[data.Length];
            engine.ProcessBytes(data, 0, data.Length, enc, 0);
            return enc;
        }

        /// <summary>
        /// Encrypt data with ChaCha20
        /// </summary>
        /// <param name="input">Input stream to encrypt</param>
        /// <param name="output">Output stream</param>
        /// <param name="key">Key</param>
        /// <param name="nonce">Nonce</param>
        /// <param name="bufferSize">Buffer size</param>
        public static void Encrypt(Stream input, Stream output, byte[] key, byte[] nonce, int bufferSize = 4096)
        {
            ChaChaEngine engine = new ChaChaEngine();
            ParametersWithIV parameters = new ParametersWithIV(new KeyParameter(key, 0, key.Length), nonce, 0, nonce.Length);
            engine.Init(true, parameters);

            int bytesRead;
            byte[] buffer = new byte[bufferSize];
            byte[] enc = new byte[bufferSize];
            do
            {
                bytesRead = input.Read(buffer, 0, bufferSize);
                if (bytesRead > 0)
                {
                    engine.ProcessBytes(buffer, 0, bytesRead, enc, 0);
                    output.Write(enc, 0, bytesRead);
                }

            } while (bytesRead > 0);
        }

        /// <summary>
        /// Decrypt data with ChaCha20
        /// </summary>
        /// <param name="data">Data to decrypt</param>
        /// <param name="key">Key</param>
        /// <param name="nonce">Nonce</param>
        /// <returns>Decrypted data</returns>
        public static byte[] Decrypt(byte[] data, byte[] key, byte[] nonce)
        {
            ChaChaEngine engine = new ChaChaEngine();
            ParametersWithIV parameters = new ParametersWithIV(new KeyParameter(key, 0, key.Length), nonce, 0, nonce.Length);
            engine.Init(false, parameters);
            byte[] dec = new byte[data.Length];
            engine.ProcessBytes(data, 0, data.Length, dec, 0);
            return dec;
        }

        /// <summary>
        /// Decrypt data with ChaCha20
        /// </summary>
        /// <param name="input">Input stream to decrypt</param>
        /// <param name="output">Output stream</param>
        /// <param name="key">Key</param>
        /// <param name="nonce">Nonce</param>
        /// <param name="bufferSize">Buffer size</param>
        public static void Decrypt(Stream input, Stream output, byte[] key, byte[] nonce, int bufferSize = 4096)
        {
            ChaChaEngine engine = new ChaChaEngine();
            ParametersWithIV parameters = new ParametersWithIV(new KeyParameter(key, 0, key.Length), nonce, 0, nonce.Length);
            engine.Init(false, parameters);

            int bytesRead;
            byte[] buffer = new byte[bufferSize];
            byte[] dec = new byte[bufferSize];
            do
            {
                bytesRead = input.Read(buffer, 0, bufferSize);
                if (bytesRead > 0)
                {
                    engine.ProcessBytes(buffer, 0, bytesRead, dec, 0);
                    output.Write(dec, 0, bytesRead);
                }

            } while (bytesRead > 0);
        }
    }
}
