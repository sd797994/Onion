using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.IInfrastructure;
using Domain.User;

namespace Application.EventSubscriber.RegisterSubscriber
{
    public class UserRegisterSubscriber : DynamicSubscriber, IUserRegisterSubscriber
    {
        public override string TopicName { get; set; }

        public UserRegisterSubscriber()
        {
            TopicName = "Onion.User.RegisterHandle";
        }
        public async Task Execute(UserRegisterEvent input)
        {
            await Task.Delay(1);
            Console.WriteLine($"欢迎注册:{input.Name}");
        }
    }
}
