using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Dtos
{
    public class RegisterInput
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
    }
}
