// -----------------------------------------------------------------------
// <copyright file="NoSubscriptionBusModule.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BusService.Modules {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using MassTransit;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class NoSubscriptionBusModule: Module {

        public string Host { get; set; }
        public string Queue { get; set; }
        public int ConsumerCount { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => ServiceBusFactory.New(sbc =>
            {
                sbc.UseRabbitMq();
                sbc.ReceiveFrom("rabbitmq://" + Host + "/" + Queue);                
            })).As<IServiceBus>()
            .SingleInstance();
        }
    }
}
