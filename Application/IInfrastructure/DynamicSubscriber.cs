using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.User;

namespace Application.IInfrastructure
{
    /// <summary>
    /// 约定所有的订阅类需继承这个接口
    /// </summary>
    public abstract class DynamicSubscriber
    {
        /// <summary>
        /// 订阅topic名称
        /// </summary>
        public abstract string TopicName { get; set; }
        
    }
}
