using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.IInfrastructure;
using Domain.User;

namespace Application.EventSubscriber.RegisterSubscriber
{
    public interface IUserRegisterSubscriber: IDynamicSubscriber<UserRegisterEvent>
    {

    }
}
