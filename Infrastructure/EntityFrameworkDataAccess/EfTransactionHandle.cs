using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using Application.IInfrastructure;
using DotNetCore.CAP;
using DotNetCore.CAP.Models;
using Infrastructure.Common;

namespace Infrastructure.EntityFrameworkDataAccess
{
    public class EfTransactionHandle : IEfTransactionHandle
    {
        private readonly Context _context;
        public EfTransactionHandle(Context context)
        {
            _context = context;
        }

        public void Init(IBeginTransaction basetran)
        {
            basetran.BeginTran += BeginTransactionHandle;
            basetran.CommitTran += CommitHandle;
        }
        public void BeginTransactionHandle(IEventBus bus)
        {
            if (bus == null)
            {
                _context.Database.BeginTransaction();
            }
        }
        public void CommitHandle(IEventBus bus)
        {
            if (bus == null)
            {
                _context.Database.CommitTransaction();
            }
        }
    }
}
