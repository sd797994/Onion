using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace Domain
{
    public interface ISpecification<TEntity>
        where TEntity : class
    {

        Expression<Func<TEntity, bool>> SatisfiedBy();
    }
}
