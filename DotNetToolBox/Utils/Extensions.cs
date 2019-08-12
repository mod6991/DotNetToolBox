#region license

//DotNetToolbox .NET helper library 
//Copyright (C) 2012  Josué Clément
//mod6991@gmail.com

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace DotNetToolBox.Utils
{
    public static class Extensions
    {
        #region Html encode/decode

        private static Dictionary<int, string> m_htmlEntitiesDic = new Dictionary<int, string>() { { 34, "&quot;" }, { 38, "&amp;" }, { 60, "&lt;" }, { 62, "&gt;" }, { 160, "&nbsp;" }, { 161, "&iexcl;" }, { 162, "&cent;" }, { 163, "&pound;" }, { 164, "&curren;" }, { 165, "&yen;" }, { 166, "&brvbar;" }, { 167, "&sect;" }, { 168, "&uml;" }, { 169, "&copy;" }, { 170, "&ordf;" }, { 171, "&laquo;" }, { 172, "&not;" }, { 173, "&shy;" }, { 174, "&reg;" }, { 175, "&macr;" }, { 176, "&deg;" }, { 177, "&plusmn;" }, { 178, "&sup2;" }, { 179, "&sup3;" }, { 180, "&acute;" }, { 181, "&micro;" }, { 182, "&para;" }, { 183, "&middot;" }, { 184, "&cedil;" }, { 185, "&sup1;" }, { 186, "&ordm;" }, { 187, "&raquo;" }, { 188, "&frac14;" }, { 189, "&frac12;" }, { 190, "&frac34;" }, { 191, "&iquest;" }, { 215, "&times;" }, { 247, "&divide;" }, { 192, "&Agrave;" }, { 193, "&Aacute;" }, { 194, "&Acirc;" }, { 195, "&Atilde;" }, { 196, "&Auml;" }, { 197, "&Aring;" }, { 198, "&AElig;" }, { 199, "&Ccedil;" }, { 200, "&Egrave;" }, { 201, "&Eacute;" }, { 202, "&Ecirc;" }, { 203, "&Euml;" }, { 204, "&Igrave;" }, { 205, "&Iacute;" }, { 206, "&Icirc;" }, { 207, "&Iuml;" }, { 208, "&ETH;" }, { 209, "&Ntilde;" }, { 210, "&Ograve;" }, { 211, "&Oacute;" }, { 212, "&Ocirc;" }, { 213, "&Otilde;" }, { 214, "&Ouml;" }, { 216, "&Oslash;" }, { 217, "&Ugrave;" }, { 218, "&Uacute;" }, { 219, "&Ucirc;" }, { 220, "&Uuml;" }, { 221, "&Yacute;" }, { 222, "&THORN;" }, { 223, "&szlig;" }, { 224, "&agrave;" }, { 225, "&aacute;" }, { 226, "&acirc;" }, { 227, "&atilde;" }, { 228, "&auml;" }, { 229, "&aring;" }, { 230, "&aelig;" }, { 231, "&ccedil;" }, { 232, "&egrave;" }, { 233, "&eacute;" }, { 234, "&ecirc;" }, { 235, "&euml;" }, { 236, "&igrave;" }, { 237, "&iacute;" }, { 238, "&icirc;" }, { 239, "&iuml;" }, { 240, "&eth;" }, { 241, "&ntilde;" }, { 242, "&ograve;" }, { 243, "&oacute;" }, { 244, "&ocirc;" }, { 245, "&otilde;" }, { 246, "&ouml;" }, { 248, "&oslash;" }, { 249, "&ugrave;" }, { 250, "&uacute;" }, { 251, "&ucirc;" }, { 252, "&uuml;" }, { 253, "&yacute;" }, { 254, "&thorn;" }, { 255, "&yuml;" }, { 8704, "&forall;" }, { 8706, "&part;" }, { 8707, "&exist;" }, { 8709, "&empty;" }, { 8711, "&nabla;" }, { 8712, "&isin;" }, { 8713, "&notin;" }, { 8715, "&ni;" }, { 8719, "&prod;" }, { 8721, "&sum;" }, { 8722, "&minus;" }, { 8727, "&lowast;" }, { 8730, "&radic;" }, { 8733, "&prop;" }, { 8734, "&infin;" }, { 8736, "&ang;" }, { 8743, "&and;" }, { 8744, "&or;" }, { 8745, "&cap;" }, { 8746, "&cup;" }, { 8747, "&int;" }, { 8756, "&there4;" }, { 8764, "&sim;" }, { 8773, "&cong;" }, { 8776, "&asymp;" }, { 8800, "&ne;" }, { 8801, "&equiv;" }, { 8804, "&le;" }, { 8805, "&ge;" }, { 8834, "&sub;" }, { 8835, "&sup;" }, { 8836, "&nsub;" }, { 8838, "&sube;" }, { 8839, "&supe;" }, { 8853, "&oplus;" }, { 8855, "&otimes;" }, { 8869, "&perp;" }, { 8901, "&sdot;" }, { 913, "&Alpha;" }, { 914, "&Beta;" }, { 915, "&Gamma;" }, { 916, "&Delta;" }, { 917, "&Epsilon;" }, { 918, "&Zeta;" }, { 919, "&Eta;" }, { 920, "&Theta;" }, { 921, "&Iota;" }, { 922, "&Kappa;" }, { 923, "&Lambda;" }, { 924, "&Mu;" }, { 925, "&Nu;" }, { 926, "&Xi;" }, { 927, "&Omicron;" }, { 928, "&Pi;" }, { 929, "&Rho;" }, { 931, "&Sigma;" }, { 932, "&Tau;" }, { 933, "&Upsilon;" }, { 934, "&Phi;" }, { 935, "&Chi;" }, { 936, "&Psi;" }, { 937, "&Omega;" }, { 945, "&alpha;" }, { 946, "&beta;" }, { 947, "&gamma;" }, { 948, "&delta;" }, { 949, "&epsilon;" }, { 950, "&zeta;" }, { 951, "&eta;" }, { 952, "&theta;" }, { 953, "&iota;" }, { 954, "&kappa;" }, { 955, "&lambda;" }, { 956, "&mu;" }, { 957, "&nu;" }, { 958, "&xi;" }, { 959, "&omicron;" }, { 960, "&pi;" }, { 961, "&rho;" }, { 962, "&sigmaf;" }, { 963, "&sigma;" }, { 964, "&tau;" }, { 965, "&upsilon;" }, { 966, "&phi;" }, { 967, "&chi;" }, { 968, "&psi;" }, { 969, "&omega;" }, { 977, "&thetasym;" }, { 978, "&upsih;" }, { 982, "&piv;" }, { 338, "&OElig;" }, { 339, "&oelig;" }, { 352, "&Scaron;" }, { 353, "&scaron;" }, { 376, "&Yuml;" }, { 402, "&fnof;" }, { 710, "&circ;" }, { 732, "&tilde;" }, { 8194, "&ensp;" }, { 8195, "&emsp;" }, { 8201, "&thinsp;" }, { 8204, "&zwnj;" }, { 8205, "&zwj;" }, { 8206, "&lrm;" }, { 8207, "&rlm;" }, { 8211, "&ndash;" }, { 8212, "&mdash;" }, { 8216, "&lsquo;" }, { 8217, "&rsquo;" }, { 8218, "&sbquo;" }, { 8220, "&ldquo;" }, { 8221, "&rdquo;" }, { 8222, "&bdquo;" }, { 8224, "&dagger;" }, { 8225, "&Dagger;" }, { 8226, "&bull;" }, { 8230, "&hellip;" }, { 8240, "&permil;" }, { 8242, "&prime;" }, { 8243, "&Prime;" }, { 8249, "&lsaquo;" }, { 8250, "&rsaquo;" }, { 8254, "&oline;" }, { 8364, "&euro;" }, { 8482, "&trade;" }, { 8592, "&larr;" }, { 8593, "&uarr;" }, { 8594, "&rarr;" }, { 8595, "&darr;" }, { 8596, "&harr;" }, { 8629, "&crarr;" }, { 8968, "&lceil;" }, { 8969, "&rceil;" }, { 8970, "&lfloor;" }, { 8971, "&rfloor;" }, { 9674, "&loz;" }, { 9824, "&spades;" }, { 9827, "&clubs;" }, { 9829, "&hearts;" }, { 9830, "&diams;" } };

        /// <summary>
        /// Convert the current string object representation into an HTML-encoded string
        /// </summary>
        public static string HTMLEncode(this string str)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0, l = str.Length; i < l; i++)
            {
                if (m_htmlEntitiesDic.ContainsKey(str[i]))
                    sb.Append(m_htmlEntitiesDic[str[i]]);
                else
                    sb.Append(str[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Convert the current string object that has been HTML-encoded into a decoded string
        /// </summary>
        public static string HTMLDecode(this string str)
        {
            string retStr = str;
            foreach (KeyValuePair<int, string> kvp in m_htmlEntitiesDic)
            {
                if (retStr.Contains(kvp.Value))
                    retStr = retStr.Replace(kvp.Value, ((char)kvp.Key).ToString());
            }
            return retStr;
        }

        #endregion

        #region Url encode

        /// <summary>
        /// Convert the current string object representation into an URL-encoded string
        /// </summary>
        public static string UrlEncode(this string str)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0, l = str.Length; i < l; i++)
            {
                int c = (int)str[i];
                if ((c > 31 && c < 48) || (c > 57 && c < 65) || (c > 90 && c < 97) || (c > 122 && c < 127))
                    sb.AppendFormat("%{0:X}", c);
                else
                    sb.Append((char)c);
            }
            return sb.ToString();
        }

        #endregion

        #region Base64 encode/decode

        /// <summary>
        /// Encode data with base64
        /// </summary>
        public static string EncodeBase64(this byte[] data)
        {
            return IO.Base64Encoder.Encode(data);
        }

        /// <summary>
        /// Decode data with base64
        /// </summary>
        public static byte[] DecodeBase64(this string base64String)
        {
            return IO.Base64Encoder.Decode(base64String);
        }

        #endregion

        #region Hexadecimal encode/decode

        /// <summary>
        /// Encode data with hexadecimal
        /// </summary>
        public static string EncodeHexadecimal(this byte[] data)
        {
            return IO.HexadecimalEncoder.Encode(data);
        }

        /// <summary>
        /// Decode data with hexadecimal
        /// </summary>
        public static byte[] DecodeHexadecimal(this string hexadecimal)
        {
            return IO.HexadecimalEncoder.Decode(hexadecimal);
        }

        #endregion

        #region GZip compress/decompress

        /// <summary>
        /// Compress data with GZip
        /// </summary>
        public static byte[] CompressGZip(this byte[] data)
        {
            return IO.GZipCompressor.Compress(data);
        }

        /// <summary>
        /// Decompress data with GZip
        /// </summary>
        public static byte[] DecompressGzip(this byte[] compressedData)
        {
            return IO.GZipCompressor.Decompress(compressedData);
        }

        #endregion

        #region In

        /// <summary>
        /// Return wether the current string is equal to a parameter
        /// </summary>
        /// <param name="parameters">Parameters</param>
        public static bool In(this string str, params string[] parameters)
        {
            bool result = false;
            for (int i = 0, l = parameters.Length; i < l; i++)
                if (str == parameters[i])
                    result = true;
            return result;
        }

        /// <summary>
        /// Return wether the current Byte is equal to a parameter
        /// </summary>
        /// <param name="parameters">Parameters</param>
        public static bool In(this Byte val, params Byte[] parameters)
        {
            bool result = false;
            for (int i = 0, l = parameters.Length; i < l; i++)
                if (val == parameters[i])
                    result = true;
            return result;
        }

        /// <summary>
        /// Return wether the current Int16 is equal to a parameter
        /// </summary>
        /// <param name="parameters">Parameters</param>
        public static bool In(this Int16 val, params Int16[] parameters)
        {
            bool result = false;
            for (int i = 0, l = parameters.Length; i < l; i++)
                if (val == parameters[i])
                    result = true;
            return result;
        }

        /// <summary>
        /// Return wether the current Int32 is equal to a parameter
        /// </summary>
        /// <param name="parameters">Parameters</param>
        public static bool In(this Int32 val, params Int32[] parameters)
        {
            bool result = false;
            for (int i = 0, l = parameters.Length; i < l; i++)
                if (val == parameters[i])
                    result = true;
            return result;
        }

        /// <summary>
        /// Return wether the current Int64 is equal to a parameter
        /// </summary>
        /// <param name="parameters">Parameters</param>
        public static bool In(this Int64 val, params Int64[] parameters)
        {
            bool result = false;
            for (int i = 0, l = parameters.Length; i < l; i++)
                if (val == parameters[i])
                    result = true;
            return result;
        }

        /// <summary>
        /// Return wether the current decimal is equal to a parameter
        /// </summary>
        /// <param name="parameters">Parameters</param>
        public static bool In(this decimal val, params decimal[] parameters)
        {
            bool result = false;
            for (int i = 0, l = parameters.Length; i < l; i++)
                if (val == parameters[i])
                    result = true;
            return result;
        }

        /// <summary>
        /// Return wether the current double is equal to a parameter
        /// </summary>
        /// <param name="parameters">Parameters</param>
        public static bool In(this double val, params double[] parameters)
        {
            bool result = false;
            for (int i = 0, l = parameters.Length; i < l; i++)
                if (val == parameters[i])
                    result = true;
            return result;
        }

        /// <summary>
        /// Return wether the current float is equal to a parameter
        /// </summary>
        /// <param name="parameters">Parameters</param>
        public static bool In(this float val, params float[] parameters)
        {
            bool result = false;
            for (int i = 0, l = parameters.Length; i < l; i++)
                if (val == parameters[i])
                    result = true;
            return result;
        }

        #endregion

        #region String utilities

        /// <summary>
        /// Remove diatrics from String
        /// </summary>
        public static string RemoveDiatrics(this string str)
        {
            string normalizedString = str.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0, l = normalizedString.Length; i < l; i++)
            {
                char c = normalizedString[i];
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        #endregion
    }
}
