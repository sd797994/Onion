using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using static System.Text.Encoding;
using Convert = System.Convert;

namespace Infrastructure.Common
{
    public class EncryptHelper: IEncryptHelper
    {
        #region DES加密解密

        //公私密钥对
        private const string KEY_64 = "vka12suj";//必须是8位无符号字符串
        private const string IV_64 = "3D63985CC7FE4C95BFF9567E9ED79CEA";
        /// <summary>
        /// DSE加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetDesEncode(string str)
        {
            byte[] byKey = UTF8.GetBytes(KEY_64);
            byte[] byIV = UTF8.GetBytes(IV_64);
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            int i = cryptoProvider.KeySize;
            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

            StreamWriter sw = new StreamWriter(cst);
            sw.Write(str);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);

        }
        /// <summary>
        /// DSE解密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetDesDecode(string str)
        {
            byte[] byKey = UTF8.GetBytes(KEY_64);
            byte[] byIV = UTF8.GetBytes(IV_64);
            byte[] byEnc;
            try
            {
                byEnc = Convert.FromBase64String(str);
            }
            catch
            {
                return null;
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(byEnc);
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cst);
            return sr.ReadToEnd();
        }
        #endregion

        #region md5加密
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="myString"></param>
        /// <returns></returns>
        public string GetMd5(string myString)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = Encoding.UTF8.GetBytes(myString);
            byte[] md5data = md5.ComputeHash(data);
            md5.Clear();
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < md5data.Length; i++)
            {
                sBuilder.Append(md5data[i].ToString("X2"));
            }
            return sBuilder.ToString();
        }
        #endregion

        #region HMACSHA256加密
        /// <summary>
        /// HMACSHA256加密
        /// </summary>
        /// <param name="message"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public string GetHmacsha256(string message)
        {
            byte[] keyByte = Encoding.UTF8.GetBytes(IV_64);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }
        #endregion
    }
}
