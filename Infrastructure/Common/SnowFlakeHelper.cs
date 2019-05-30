using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Infrastructure.Common
{
    public class SnowFlakeHelper: ISnowFlakeHelper
    {
        private static long _machineId; //机器ID
        private static long _datacenterId = 0L; //数据ID
        private static long _sequence = 0L; //计数从零开始
        private static readonly long Twepoch = 687888001020L; //唯一时间随机量
        private static readonly long MachineIdBits = 5L; //机器码字节数
        private static readonly long DatacenterIdBits = 5L; //数据字节数
        public static long MaxMachineId = -1L ^ -1L << (int) MachineIdBits; //最大机器ID
        private static readonly long MaxDatacenterId = -1L ^ (-1L << (int) DatacenterIdBits); //最大数据ID
        private static readonly long SequenceBits = 12L; //计数器字节数，12个字节用来保存计数码        
        private static readonly long MachineIdShift = SequenceBits; //机器码数据左移位数，就是后面计数器占用的位数
        private static readonly long DatacenterIdShift = SequenceBits + MachineIdBits;
        private static readonly long TimestampLeftShift = SequenceBits + MachineIdBits + DatacenterIdBits; //时间戳左移动位数就是机器码+计数器总字节数+数据字节数
        public static long SequenceMask = -1L ^ -1L << (int) SequenceBits; //一微秒内可以产生计数，如果达到该值则等到下一微秒在进行生成
        private static long _lastTimestamp = -1L; //最后时间戳
        private static readonly object SyncRoot = new object(); //加锁对象

        public SnowFlakeHelper()
        {
            Snowflakes(0L, -1);
        }

        public SnowFlakeHelper(long machineId)
        {
            Snowflakes(machineId, -1);
        }

        public SnowFlakeHelper(long machineId, long datacenterId)
        {
            Snowflakes(machineId, datacenterId);
        }

        private void Snowflakes(long machineId, long datacenterId)
        {
            if (machineId >= 0)
            {
                if (machineId > MaxMachineId)
                {
                    throw new Exception("机器码ID非法");
                }
                _machineId = machineId;
            }
            if (datacenterId >= 0)
            {
                if (datacenterId > MaxDatacenterId)
                {
                    throw new Exception("数据中心ID非法");
                }
                _datacenterId = datacenterId;
            }
        }

        /// <summary>
        /// 生成当前时间戳
        /// </summary>
        /// <returns>毫秒</returns>
        private static long GetTimestamp()
        {
            return (long) (DateTime.UtcNow - new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }

        /// <summary>
        /// 获取下一微秒时间戳
        /// </summary>
        /// <param name="lastTimestamp"></param>
        /// <returns></returns>
        private static long GetNextTimestamp(long lastTimestamp)
        {
            long timestamp = GetTimestamp();
            int count = 0;
            while (timestamp <= lastTimestamp)
            {
                count++;
                if (count > 10)
                    throw new Exception("机器时间异常");
                Thread.Sleep(1);
                timestamp = GetTimestamp();
            }
            return timestamp;
        }

        /// <summary>
        /// 获取长整形的ID
        /// </summary>
        /// <returns></returns>
        public long GetId()
        {
            lock (SyncRoot)
            {
                var timestamp = GetTimestamp();
                if (_lastTimestamp == timestamp)
                {
                    _sequence = (_sequence + 1) & SequenceMask;
                    if (_sequence == 0)
                    {
                        timestamp = GetNextTimestamp(_lastTimestamp);
                    }
                }
                else
                {
                    _sequence = 0L;
                }
                if (timestamp < _lastTimestamp)
                {
                    return GetId();
                }
                _lastTimestamp = timestamp;
                long id = ((timestamp - Twepoch) << (int) TimestampLeftShift)
                          | (_datacenterId << (int) DatacenterIdShift)
                          | (_machineId << (int) MachineIdShift)
                          | _sequence;
                return id;
            }
        }
    }
}
