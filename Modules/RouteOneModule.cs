using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using MassTransit;
using MassTransit.Log4NetIntegration;
using BusService.Consumers;
using log4net;
using log4net.Config;

namespace BusService.Modules
{
    class RouteOneModule : Module
    {
        public string Host { get; set; }
        public string Queue { get; set; }
        public int ConsumerCount { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            XmlConfigurator.Configure();
            //will not work, no consumers actually response to a message with this setup
            builder.RegisterType<Passenger>().As<IConsumer>();
            
            builder.Register(c => ServiceBusFactory.New(sbc =>
            {
                sbc.UseRabbitMq();
                sbc.ReceiveFrom("rabbitmq://" + Host + "/" + Queue);

                if (ConsumerCount > 0) {
                    sbc.SetConcurrentConsumerLimit(ConsumerCount);
                }
                
                sbc.UseLog4Net();
                sbc.Subscribe(x => x.LoadFrom(c.Resolve<ILifetimeScope>()));
            })).As<IServiceBus>()
            .SingleInstance();

        }
    }
}
