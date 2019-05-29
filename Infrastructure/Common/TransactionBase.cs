using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using Application.IInfrastructure;
using DotNetCore.CAP;
using DotNetCore.CAP.Models;
using Infrastructure.CapEventBusAccess;
using Infrastructure.EntityFrameworkDataAccess;

namespace Infrastructure.Common
{
    public class TransactionBase : IBeginTransaction
    {
        public event BeginTransaction BeginTran;
        public event CommitTransaction CommitTran;
        public TransactionBase(ICapTransactionHandle capTransaction, IEfTransactionHandle efTransaction)
        {
            capTransaction.Init(this);
            efTransaction.Init(this);
        }
        ~TransactionBase()
        {
            Dispose();
        }
        public IBeginTransaction BeginTransaction(IEventBus eventBus = null)
        {
            BeginTran?.Invoke(eventBus);
            return this;
        }

        public void Commit(IEventBus eventBus = null)
        {
            CommitTran?.Invoke(eventBus);
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
