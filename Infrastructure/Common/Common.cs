using Application.IInfrastructure;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Common
{
    public class Common : ICommon
    {
        private readonly IEncryptHelper _encryptHelper;

        public Common(IEncryptHelper encryptHelper)
        {
            _encryptHelper = encryptHelper;
        }

        /// <summary>
        /// 根据内容编码jwtToken
        /// </summary>
        /// <param name="expressTime"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public string GetJwtToken(object keys)
        {
            var jwtHeader = JsonConvert.SerializeObject(
                new { TimeStamp = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") });
            var base64Payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(keys)));
            var encodedString = $"{Convert.ToBase64String(Encoding.UTF8.GetBytes(jwtHeader))}.{base64Payload}";
            var signature = _encryptHelper.GetHmacsha256(encodedString);
            var token = $"{encodedString}.{signature}";
            return token;
        }
        /// <summary>
        /// sha256加密
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public string ShaEncrypt(params string[] keys)
        {
            return Convert.ToBase64String(
                new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(string.Join("", keys))));
        }
        /// <summary>
        /// 获取特定配置节
        /// </summary>
        /// <param name="basepath"></param>
        /// <param name="filename"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetConfigurationSetting(string basepath,string filename,string key)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basepath)
                .AddJsonFile(filename)
                .Build();
           return configuration
                .GetValue<string>(key);
        }

        /// <summary>
        /// 获取第一个可用的端口号
        /// </summary>
        /// <returns></returns>
        readonly List<int> _usePort =new List<int>();
        public int GetFirstAvailablePort()
        {
            int MAX_PORT = 65535; //系统tcp/udp端口数最大是65535            
            int BEGIN_PORT = 5000;//从这个端口开始检测
            while (true)
            {
                var randomPort = new Random(Guid.NewGuid().GetHashCode()).Next(BEGIN_PORT, MAX_PORT);
                if (PortIsAvailable(randomPort))
                {
                    return randomPort;
                }
                else
                {
                    _usePort.Add(randomPort);
                }
                if (_usePort.Count == MAX_PORT - BEGIN_PORT)
                {
                    break;
                }
            }
            return -1;
        }

        /// <summary>
        /// 检查指定端口是否已用
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        static bool PortIsAvailable(int port)
        {
            bool isAvailable = true;

            IList portUsed = PortIsUsed();

            foreach (int p in portUsed)
            {
                if (p == port)
                {
                    isAvailable = false; break;
                }
            }

            return isAvailable;
        }

        /// <summary>
        /// 获取操作系统已用的端口号
        /// </summary>
        /// <returns></returns>
        static IList PortIsUsed()
        {
            //获取本地计算机的网络连接和通信统计数据的信息
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

            //返回本地计算机上的所有Tcp监听程序
            IPEndPoint[] ipsTCP = ipGlobalProperties.GetActiveTcpListeners();

            //返回本地计算机上的所有UDP监听程序
            IPEndPoint[] ipsUDP = ipGlobalProperties.GetActiveUdpListeners();

            //返回本地计算机上的Internet协议版本4(IPV4 传输控制协议(TCP)连接的信息。
            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            IList allPorts = new ArrayList();
            foreach (IPEndPoint ep in ipsTCP) allPorts.Add(ep.Port);
            foreach (IPEndPoint ep in ipsUDP) allPorts.Add(ep.Port);
            foreach (TcpConnectionInformation conn in tcpConnInfoArray) allPorts.Add(conn.LocalEndPoint.Port);

            return allPorts;
        }
    }
}
