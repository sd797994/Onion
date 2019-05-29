using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Application.EventSubscriber;
using Application.IInfrastructure;
using Autofac;
using Domain.User;
using DotNetCore.CAP;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Infrastructure.CapEventBusAccess
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(InfrastructureException).Assembly)
                .Where(type => type.Namespace.Contains("CapEventBusAccess"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            //构造动态cap订阅类
            builder.RegisterTypes(CapSubscriberBuilder.CapSubscriberTypeBuilder().ToArray()).AsImplementedInterfaces().InstancePerLifetimeScope();
        }

    }
}
