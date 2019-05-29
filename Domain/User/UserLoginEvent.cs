using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.User
{
    public class UserLoginEvent : IEvent
    {
        public UserLoginEvent(string name,DateTime loginTime)
        {
            Name = name;
            LoginTime = loginTime;
        }
        public string Name { get; set; }
        public DateTime LoginTime { get; set; }
    }
}
