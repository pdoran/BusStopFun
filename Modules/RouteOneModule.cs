using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using MassTransit;
using BusService.Consumers;
namespace BusService.Modules
{
    class RouteOneModule : Module
    {
        public string Host { get; set; }
        public string Queue { get; set; }
        public int ConsumerCount { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Passenger>().As<IConsumer>();
    
            builder.Register(c => ServiceBusFactory.New(sbc =>
            {
                sbc.UseRabbitMq();
                sbc.ReceiveFrom("rabbitmq://" + Host + "/" + Queue);

                //this will find all of the consumers in the container and
                //register them with the bus.
                sbc.Subscribe(x =>
                {
                    x.Consumer<Passenger>();
                });
                sbc.Subscribe(x => x.LoadFrom(c.Resolve<ILifetimeScope>()));
            })).As<IServiceBus>()
            .SingleInstance();

        }
    }
}
