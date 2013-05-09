using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Topshelf;
using Autofac;
using BusService.Modules;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;

namespace BusService
{
    class Service : ServiceControl
    {
        private readonly string _host;
        private readonly string _queue;
        private readonly int _consumercount;
        private IServiceBus _bus;
        public Service(string Host, string queue, int ConsumerCount)
        {
            _host = Host;
            _queue = queue;
            _consumercount = ConsumerCount;
        }
        public bool Start(HostControl hostControl)
        {
            
                Thread.Sleep(10000);
                var builder = new ContainerBuilder();
                builder.RegisterModule(new RouteOneModule
                {
                    Host = _host,
                    Queue = _queue,
                    ConsumerCount = _consumercount

                });
                var container = builder.Build();
                _bus = container.Resolve<IServiceBus>();

            return true;
        }

        public bool Stop(HostControl hostControl)
        {

            return true;
        }
    }
}
