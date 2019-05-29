using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Common
{
    public interface IEncryptHelper
    {
        /// <summary>
        /// HMACSHA256加密
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        string GetHmacsha256(string message);
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="myString"></param>
        /// <returns></returns>
        string GetMd5(string myString);
        /// <summary>
        /// DSE加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        string GetDesEncode(string str);
        /// <summary>
        /// DSE解密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        string GetDesDecode(string str);
    }
}
