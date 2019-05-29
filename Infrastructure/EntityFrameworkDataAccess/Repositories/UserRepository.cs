using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Application.IInfrastructure.IRepositories;
using Domain;
using Domain.User;
using DotNetCore.CAP;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFrameworkDataAccess.Repositories
{
    public class UserRepository : RepositoryBase<User, PersistentObjects.User>, IUserRepository
    {
        private readonly Context _context;

        public UserRepository(
            Context context
        ) : base(context)
        {
            _context = context;
        }
        /// <summary>
        /// 获取所有持久化对象转为领域对象
        /// </summary>
        /// <returns></returns>
        public override IQueryable<User> GetAll(bool asNoTracking = false) =>
            from a in (asNoTracking ? _context.User.AsNoTracking() : _context.User)
            select new User()
            {
                Id = a.Id,
                UserName = a.UserName,
                Password = a.Password,
                NickName = a.NickName,
                Amount = a.Amount,
                State = a.State,
            };
    }
}
