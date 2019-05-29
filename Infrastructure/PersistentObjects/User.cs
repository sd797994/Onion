using System;
using System.Collections.Generic;
using System.Text;
using Domain.User;

namespace Infrastructure.PersistentObjects
{
    public class User
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 账户余额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public UserStateEnum State { get; set; }
    }
}
