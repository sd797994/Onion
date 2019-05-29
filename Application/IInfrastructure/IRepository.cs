using Domain;
using System.Threading.Tasks;

namespace Application.IInfrastructure
{
    /// <summary>
    /// 基本仓储对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// 新增对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Add(T t);
        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Update(T t);
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Delete(T t);
        /// <summary>
        /// 根据主键获取对象
        /// </summary>
        /// <returns></returns>
        Task<T> GetAsync(object key);
        /// <summary>
        /// 根据规约获取对象
        /// </summary>
        /// <param name="specification"></param>
        /// <param name="asNoTracking"></param>
        /// <returns></returns>
        Task<T> GetAsync(ISpecification<T> specification, bool asNoTracking = false);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="specification"></param>
        /// <param name="asNoTracking"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(ISpecification<T> specification, bool asNoTracking = false);
        /// <summary>
        /// 工作单元提交
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveAsync();
    }
}
