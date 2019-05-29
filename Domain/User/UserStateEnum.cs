using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.User
{
    public enum UserStateEnum
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal=0,
        /// <summary>
        /// 已注销
        /// </summary>
        Cancellation=1,
    }
}
