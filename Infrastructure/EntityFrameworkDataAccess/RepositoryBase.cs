using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application;
using Application.IInfrastructure;
using Domain;
using Domain.User;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFrameworkDataAccess
{
    public abstract class RepositoryBase<TDo, TPo> : IRepository<TDo> where TDo : class, new() where TPo : class, new()
    {
        private readonly Context _context;

        protected RepositoryBase(Context context)
        {
            _context = context;
        }
        /// <summary>
        /// 强制重写转换类用于查询
        /// </summary>
        /// <param name="asNoTracking"></param>
        /// <returns></returns>
        public abstract IQueryable<TDo> GetAll(bool asNoTracking = false);
        /// <summary>
        /// 新增对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Add(TDo t)
        {
            _context.Set<TPo>().Add(Mapper<TDo, TPo>.Map(t));
            return true;
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Update(TDo t)
        {
            _context.Set<TPo>().Update(Mapper<TDo, TPo>.Map(t));
            return true;
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Delete(TDo t)
        {
            _context.Set<TPo>().Remove(Mapper<TDo, TPo>.Map(t));
            return true;
        }

        /// <summary>
        /// 根据主键获取对象
        /// </summary>
        /// <returns></returns>
        public async Task<TDo> GetAsync(object key)
        {
            var result = await _context.Set<TPo>().FindAsync(key);
            if (result != null)
            {
                return Mapper<TPo, TDo>.Map(result);
            }
            return null;
        }
        /// <summary>
        /// 根据条件查询对象
        /// </summary>
        /// <param name="specification"></param>
        /// <param name="asNoTracking"></param>
        /// <returns></returns>
        public async Task<TDo> GetAsync(ISpecification<TDo> specification, bool asNoTracking = false)
        {
            return  GetAll(asNoTracking).FirstOrDefault(specification.SatisfiedBy());
        }

        /// <summary>
        /// 根据条件判断对象是否存在
        /// </summary>
        /// <param name="specification"></param>
        /// <param name="asNoTracking"></param>
        /// <returns></returns>
        public async Task<bool> AnyAsync(ISpecification<TDo> specification, bool asNoTracking = false)
        {
            return await GetAll(asNoTracking).AnyAsync(specification.SatisfiedBy());
        }
        /// <summary>
        /// 异步工作单元
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveAsync()
        {
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
