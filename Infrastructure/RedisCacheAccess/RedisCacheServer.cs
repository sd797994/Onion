using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Application;
using Application.IInfrastructure;
using Infrastructure.Common;
using Infrastructure.EntityFrameworkDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Infrastructure.RedisCacheAccess
{
    public class RedisCacheServer: ICacheServer
    {
        private static IDatabase _cache;
        private readonly ConnectionMultiplexer _connection;
        public RedisCacheServer(ICommon common)
        {
            int database = 0;
            string connectionString = common.GetConfigurationSetting(Directory.GetCurrentDirectory(), "autofac.json", "modules:3:properties:ConnectionString");
            _connection = ConnectionMultiplexer.Connect(connectionString);
            _cache = _connection.GetDatabase(database);
        }
        /// <summary>
        /// 判断缓存是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            return _cache.KeyExists(key);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetCache<T>(string key) where T : class
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));


            var value = _cache.StringGet(key);
            if (!value.HasValue)
                return default(T);


            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetCache(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            if (value == null)
                throw new ArgumentNullException(nameof(value));


            if (Exists(key))
                RemoveCache(key);


            _cache.StringSet(key, JsonConvert.SerializeObject(value));
        }
        /// <summary>
        /// 设置缓存带过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiressAbsoulte"></param>
        public void SetCache(string key, object value, TimeSpan expiressAbsoulte)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            if (value == null)
                throw new ArgumentNullException(nameof(value));


            if (Exists(key))
                RemoveCache(key);


            _cache.StringSet(key, JsonConvert.SerializeObject(value), expiressAbsoulte);
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        public void RemoveCache(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));


            _cache.KeyDelete(key);
        }

        /// <summary>
        /// 强制回收redis连接
        /// </summary>
        public void Dispose()
        {
            if (_connection != null)
                _connection.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
