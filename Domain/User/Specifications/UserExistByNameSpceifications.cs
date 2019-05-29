using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Domain.Specification;

namespace Domain.User.Specifications
{
    public class UserExistByNameSpceifications: ISpecification<User>
    {
        private string _userName { get; set; }
        public UserExistByNameSpceifications(string userName)
        {
            _userName = userName;
        }
        public Expression<Func<User, bool>> SatisfiedBy()
        {
            var where = PredicateBuilder.True<User>();
            if (!string.IsNullOrEmpty(_userName))
            {
                where = where.And(x => x.UserName == _userName);
            }
            return where;
        }
    }
}
