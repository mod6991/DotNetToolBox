using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetToolBox.Cryptography
{
    public class PaddingException : Exception
    {
        public PaddingException(string message) : base(message) { }
    }

    public enum PaddingStyle
    {
        Pkcs7,
        AnsiX923,
        Iso7816,
        Iso10126
    }

    public static class Padding
    {
        public static byte[] Pad(byte[] data, int blockSize, PaddingStyle paddingStyle)
        {
            switch (paddingStyle)
            {
                case PaddingStyle.Pkcs7:
                    return PadPkcs7(data, blockSize);
                case PaddingStyle.AnsiX923:
                    return PadAnsiX923(data, blockSize);
                case PaddingStyle.Iso7816:
                    return PadIso7816(data, blockSize);
                case PaddingStyle.Iso10126:
                    return PadIso10126(data, blockSize);
                default:
                    throw new InvalidOperationException("Unknown padding style");
            }
        }

        public static byte[] Unpad(byte[] data, int blockSize, PaddingStyle paddingStyle)
        {
            switch (paddingStyle)
            {
                case PaddingStyle.Pkcs7:
                    return UnpadPkcs7(data, blockSize);
                case PaddingStyle.AnsiX923:
                    return UnpadAnsiX923(data, blockSize);
                case PaddingStyle.Iso7816:
                    return UnpadIso7816(data, blockSize);
                case PaddingStyle.Iso10126:
                    return UnpadIso10126(data, blockSize);
                default:
                    throw new InvalidOperationException("Unknown padding style");
            }
        }

        public static byte[] PadPkcs7(byte[] data, int blockSize)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            int paddingLength = blockSize - data.Length % blockSize;

            byte[] paddedData = new byte[data.Length + paddingLength];
            Array.Copy(data, 0, paddedData, 0, data.Length);
            for (int i = data.Length; i < paddedData.Length; i++)
                paddedData[i] = (byte)paddingLength;

            return paddedData;
        }

        public static byte[] UnpadPkcs7(byte[] data, int blockSize)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            if (data.Length % blockSize != 0 || data.Length == 0)
                throw new PaddingException("Data is not padded");

            byte paddingLength = data[data.Length - 1];

            for (int i = data.Length - 2; i > data.Length - paddingLength - 1; i--)
            {
                if (data[i] != paddingLength)
                    throw new PaddingException("Invalid Pkcs7 padding");
            }

            byte[] unpaddedData = new byte[data.Length - paddingLength];
            Array.Copy(data, 0, unpaddedData, 0, data.Length - paddingLength);

            return unpaddedData;
        }

        public static byte[] PadAnsiX923(byte[] data, int blockSize)
        {
            //00 00 00 04
            throw new NotImplementedException();
        }

        public static byte[] UnpadAnsiX923(byte[] data, int blockSize)
        {
            //00 00 00 04
            throw new NotImplementedException();
        }

        public static byte[] PadIso7816(byte[] data, int blockSize)
        {
            //80 00 00 00
            throw new NotImplementedException();
        }

        public static byte[] UnpadIso7816(byte[] data, int blockSize)
        {
            //80 00 00 00
            throw new NotImplementedException();
        }

        public static byte[] PadIso10126(byte[] data, int blockSize)
        {
            //RN RN RN 04
            throw new NotImplementedException();
        }

        public static byte[] UnpadIso10126(byte[] data, int blockSize)
        {
            //RN RN RN 04
            throw new NotImplementedException();
        }
    }
}
