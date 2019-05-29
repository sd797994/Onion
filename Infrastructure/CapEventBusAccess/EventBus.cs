using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.IInfrastructure;
using DotNetCore.CAP;

namespace Infrastructure.CapEventBusAccess
{
    /// <summary>
    /// 事件总线
    /// </summary>
    public class EventBus : IEventBus
    {
        public readonly ICapPublisher _publisher;
        public EventBus(ICapPublisher publisher)
        {
            _publisher = publisher;
        }
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="contentObj"></param>
        /// <param name="callbackName"></param>
        public void Publish<T>(string name, T contentObj, string callbackName = null)
        {
            _publisher.Publish(name, contentObj, callbackName);
        }
        /// <summary>
        /// 异步发布事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="contentObj"></param>
        /// <param name="callbackName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task PublishAsync<T>(string name, T contentObj, string callbackName = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await _publisher.PublishAsync(name, contentObj, callbackName, cancellationToken);
        }
    }
}
