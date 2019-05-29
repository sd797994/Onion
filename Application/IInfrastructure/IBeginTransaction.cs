using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IInfrastructure
{
    public delegate void BeginTransaction(IEventBus eventBus = null);
    public delegate void CommitTransaction(IEventBus eventBus = null);
    public interface IBeginTransaction : IDisposable
    {
        event BeginTransaction BeginTran;
        event CommitTransaction CommitTran;
        IBeginTransaction BeginTransaction(IEventBus eventbus = null);
        void Commit(IEventBus eventBus = null);
    }
}
