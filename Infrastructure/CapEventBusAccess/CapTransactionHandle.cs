using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Application.IInfrastructure;
using DotNetCore.CAP;
using DotNetCore.CAP.Models;
using Infrastructure.Common;
using Infrastructure.EntityFrameworkDataAccess;

namespace Infrastructure.CapEventBusAccess
{
    public class CapTransactionHandle : ICapTransactionHandle
    {
        private readonly Context _context;
        private ICapPublisher _publisher;
        public CapTransactionHandle(Context context)
        {
            _context = context;
        }
        public void Init(IBeginTransaction basetran)
        {
            basetran.BeginTran += BeginTransactionHandle;
            basetran.CommitTran += CommitHandle;
        }
        public void BeginTransactionHandle(IEventBus bus = null)
        {
            if (bus != null)
            {
                var trans = _context.Database.BeginTransaction();
                _publisher = (ICapPublisher)(bus.GetType().GetFields().First(x=>x.FieldType == typeof(ICapPublisher)).GetValue(bus));
                _publisher.Transaction.Begin(trans);
            }
        }
        public void CommitHandle(IEventBus bus = null)
        {
            if (bus != null)
            {
                _publisher.Transaction.Commit();
            }
        }
    }

}
