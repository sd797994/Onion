using Application.IInfrastructure;
using Autofac;
using Infrastructure.Common.ObjectExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Infrastructure.CapEventBusAccess
{
    public static class CapSubscriberBuilder
    {
        public static IEnumerable<Type> CapSubscriberTypeBuilder()
        {
            var allassembly = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes().Where(t => t.BaseType == typeof(DynamicSubscriber) && !t.IsInterface)).ToArray();
            var bodys = GetBodyFromType(allassembly);
            return allassembly.BuildType("CapSubscriber.g", bodys);
        }

        private static List<StringBuilder> GetBodyFromType(Type[] allassembly)
        {
            var bodyType = new List<StringBuilder>();
            foreach (var type in allassembly)
            {
                var baseInterface = type.GetInterfaces().FirstOrDefault();
                var obj = Activator.CreateInstance(type);
                var className = type.Name;
                var topicName = type.GetProperty("TopicName").GetValue(obj);
                var eventInfo = type.GetMethod("Execute").GetParameters().FirstOrDefault()?.ParameterType;
                var content = new StringBuilder();
                content.AppendLine($"using {baseInterface.Namespace};");
                content.AppendLine($"using {eventInfo.Namespace};");
                content.AppendLine("using DotNetCore.CAP;");
                content.AppendLine("using System.Threading.Tasks;");
                content.AppendLine("namespace Infrastructure.CapEventBusAccess");
                content.AppendLine("{");
                content.AppendLine($"	public class {className} : ICapSubscribe");
                content.AppendLine("	{");
                content.AppendLine($"		private readonly {baseInterface.Name} _subscriber;");
                content.AppendLine($"		public {className}({baseInterface.Name} subscriber)");
                content.AppendLine("		{");
                content.AppendLine("			_subscriber = subscriber;");
                content.AppendLine("		}");
                content.AppendLine($"		[CapSubscribe(\"{topicName}\")]");
                content.AppendLine($"		public async Task Handle({eventInfo.Name} input)");
                content.AppendLine("		{");
                content.AppendLine("			await _subscriber.Execute(input);");
                content.AppendLine("		}");
                content.AppendLine("	}");
                content.AppendLine("}");
                bodyType.Add(content);
            }
            return bodyType;
        }
    }
}
