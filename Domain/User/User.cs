using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.User
{
    public sealed class User : IAggregateRoot
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
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="nickname"></param>
        public void Register(string username, string password, string nickname)
        {
            Id = Guid.NewGuid();
            if (string.IsNullOrEmpty(username) || username.Length > 11 || username.Length < 5)
            {
                throw new DomainException("请输入用户名并确保长度为6-10!");
            }
            UserName = username;
            if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                throw new DomainException("请输入密码并确保长度大于8!");
            }
            if (string.IsNullOrEmpty(nickname) || nickname.Length > 9 || nickname.Length < 1)
            {
                throw new DomainException("请输入昵称并确保长度为2-8!");
            }
            NickName = nickname;
            Amount = 0;
            State = UserStateEnum.Normal;
        }

        /// <summary>
        /// 设置密码
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="nickname"></param>
        public void SetPassword(string password)
        {
            Password = password;
        }
        /// <summary>
        /// 用户注销
        /// </summary>
        public void Cancellation()
        {
            if (Amount > 0)
            {
                throw new DomainException("用户余额不为0无法注销!");
            }
            State = UserStateEnum.Cancellation;
        }
        /// <summary>
        /// 用户金额变更
        /// </summary>
        /// <param name="plus"></param>
        /// <param name="amount"></param>
        public void ChangeAmount(bool plus, decimal amount)
        {
            if (State != UserStateEnum.Normal)
            {
                if (amount <= 0)
                {
                    throw new DomainException("变更金额错误,无法变更!");
                }
                if (plus)
                {
                    Amount += amount;
                }
                else
                {
                    if (amount > Amount)
                    {
                        throw new DomainException("减扣金额大于余额,无法变更!");
                    }
                    Amount -= amount;
                }
            }
            else
            {
                throw new DomainException("只有账户状态正常的用户才可变更金额!");
            }
        }
    }
}
