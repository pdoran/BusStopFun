using Autofac;
using BusService.Messages;
using BusService.Modules;
using Castle.Windsor;
using MassTransit;
using MassTransit.BusConfigurators;
using MassTransit.SubscriptionConfigurators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace BusService
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //HostFactory.Run(x =>
            //    {
            //        x.Service(settings => new Service("localhost", "fare", 1));
            //        x.RunAsLocalSystem();
            //        x.SetDescription("BusStop Service");
            //        x.SetDisplayName("BusStop");
            //        x.SetServiceName("BusStop");
            //    });
            //var builder = new ContainerBuilder();
            
            
            
            IServiceBus bus = SetupUsingWindsor("localhost", "fare2", 1);
            bus.Publish(new FareMessageImpl
            {
                FareAmount = 10,
                CorrelationId = Guid.NewGuid()
            });
         }

        static IServiceBus SetupUsingAutoFac(string Host,string Queue,int ConsumerCount) {

            var builder = new ContainerBuilder();
            builder.RegisterModule(new RouteOneModule {
                Host = Host,
                Queue = Queue,
                ConsumerCount = ConsumerCount

            });
            var container = builder.Build();
            return container.Resolve<IServiceBus>();
        }

        static IServiceBus SetupUsingWindsor(string Host, string Queue, int ConsumerCount) {

            var container = new WindsorContainer();
            container.Install(new RouteOneInstaller {
                Host = Host,
                Queue = Queue,
                ConsumerCount = ConsumerCount
            });
            return container.Resolve<IServiceBus>();
        }
       
    }
}
