using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using MassTransit;
using BusService.Consumers;
using MassTransit.Log4NetIntegration;
using log4net;
using log4net.Config;

namespace BusService.Modules
{
    class RouteOneInstaller : IWindsorInstaller
    {
        public string Host { get; set; }
        public string Queue { get; set; }
        public int ConsumerCount { get; set; }

        public void Install(IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            XmlConfigurator.Configure();
            //this does not work
            //container.Register(Component.For<IConsumer>().ImplementedBy<Passenger>()
            //however this does work
            container.Register(Component.For<Passenger>(),
             Component.For<IServiceBus>().UsingFactoryMethod(() =>
                    {
                        return ServiceBusFactory.New(sbc =>
                            {
                                sbc.UseRabbitMq();
                                sbc.UseRabbitMqRouting();
                                sbc.ReceiveFrom("rabbitmq://" + Host + "/" + Queue);
                                if (ConsumerCount > 0)
                                {
                                    sbc.SetConcurrentConsumerLimit(ConsumerCount);
                                }
                                sbc.Subscribe(x => x.LoadFrom(container));
                                sbc.UseLog4Net();
                            });
                    })
                    .LifeStyle.Singleton);
        }
    }
}
