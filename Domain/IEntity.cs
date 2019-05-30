using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    /// <summary>
    /// 领域实体标记
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface IEntity
    {
        long Id { get; }
    }
}
