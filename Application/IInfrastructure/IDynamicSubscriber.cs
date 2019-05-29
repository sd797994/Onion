using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IInfrastructure
{
    /// <summary>
    /// 所有的订阅接口需要继承这个接口并实现
    /// </summary>
    public interface IDynamicSubscriber<in T>
    {
        Task Execute(T input);
    }
}
