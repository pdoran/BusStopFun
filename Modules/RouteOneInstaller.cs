using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using MassTransit;
using BusService.Consumers;

namespace BusService.Modules
{
    class RouteOneInstaller : IWindsorInstaller
    {
        public string Host { get; set; }
        public string Queue { get; set; }
        public int ConsumerCount { get; set; }

        public void Install(IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            // register each consumer manually
            container.Register(Component.For<Passenger>(),
             Component.For<IServiceBus>().UsingFactoryMethod(() =>
                    {
                        return ServiceBusFactory.New(sbc =>
                            {
                                sbc.UseRabbitMq();
                                sbc.ReceiveFrom("rabbitmq://" + Host + "/" + Queue);
                                if (ConsumerCount > 0)
                                {
                                    sbc.SetConcurrentConsumerLimit(ConsumerCount);
                                }
                                sbc.Subscribe(s => s.Consumer<Passenger>());
                                //sbc.Subscribe(x => x.LoadFrom(container));
                            });
                    })
                    .LifeStyle.Singleton);
        }
    }
}
