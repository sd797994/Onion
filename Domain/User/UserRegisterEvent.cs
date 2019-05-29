using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.User
{
    public class UserRegisterEvent : IEvent
    {
        public UserRegisterEvent(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
