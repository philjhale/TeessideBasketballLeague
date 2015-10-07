using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Basketball.Common.Extensions
{
    public static class SecurityExtensions
    {
        /// <summary>
        /// Extension method for encrypting a string to its MD5 equivalent.  Saltless.
        /// </summary>
        /// <param name="strToEncrypt">The STR to encrypt.</param>
        /// <returns></returns>
        public static string ToMd5(this string strToEncrypt)
        {
            System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
            byte[] bytes = ue.GetBytes(strToEncrypt);

            // encrypt bytes
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] hashBytes = md5.ComputeHash(bytes);

            // Convert the encrypted bytes back to a string (base 16)
            string hashString = hashBytes.Aggregate("", (current, t) => current + Convert.ToString(t, 16).PadLeft(2, '0'));

            return hashString.PadLeft(32, '0');
        }

    }
}
