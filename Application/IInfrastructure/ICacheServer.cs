using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IInfrastructure
{
    public interface ICacheServer
    {
        /// <summary>
        /// 判断缓存是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Exists(string key);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetCache<T>(string key) where T : class;

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetCache(string key, object value);

        /// <summary>
        /// 设置缓存带过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiressAbsoulte"></param>
        void SetCache(string key, object value, TimeSpan expiressAbsoulte);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        void RemoveCache(string key);

        /// <summary>
        /// 强制回收redis连接
        /// </summary>
        void Dispose();
    }
}
