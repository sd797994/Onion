using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IInfrastructure
{
    public interface ICommon
    {
        /// <summary>
        /// 根据内容编码jwtToken
        /// </summary>
        /// <param name="expressTime"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        string GetJwtToken(object keys);

        /// <summary>
        /// sha256加密
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        string ShaEncrypt(params string[] keys);

        /// <summary>
        /// 获取系统空闲端口
        /// </summary>
        /// <returns></returns>
        int GetFirstAvailablePort();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basepath"></param>
        /// <param name="filename"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetConfigurationSetting(string basepath, string filename, string key);

        /// <summary>
        /// 生成全局id
        /// </summary>
        /// <returns></returns>
        long CreateGlobalId();
    }
}
