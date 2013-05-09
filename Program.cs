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
            //builder.RegisterModule(new RouteOneModule
            //{
            //    Host = "localhost",
            //    Queue = "fare",
            //    ConsumerCount = 1

            //});
            //var container = builder.Build();
            //IConsumer blah = container.Resolve<IConsumer>();
            //IServiceBus LocalBus = container.Resolve<IServiceBus>();
            var container = new WindsorContainer();
            container.Install(new RouteOneInstaller
            {
                Host = "localhost",
                Queue = "fare",
                ConsumerCount = 1
            });
            IServiceBus bus = container.Resolve<IServiceBus>();
            bus.Publish(new FareMessageImpl
            {
                FareAmount = 10,
                CorrelationId = Guid.NewGuid()
            });
         }
       
    }
}
