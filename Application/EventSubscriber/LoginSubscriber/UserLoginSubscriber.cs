using Application.IInfrastructure;
using Domain.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.EventSubscriber.LoginSubscriber
{
    public class UserLoginSubscriber : DynamicSubscriber, IUserLoginSubscriber
    {
        public override string TopicName { get; set; }

        public UserLoginSubscriber()
        {
            TopicName = "Microservice.User.LoginHandle";
        }
        public async Task Execute(UserLoginEvent input)
        {
            await Task.Delay(1);
            Console.WriteLine($"欢迎登录:{input.Name},登录时间为{input.LoginTime}");
        }
    }
}
