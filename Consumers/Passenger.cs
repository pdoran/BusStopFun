using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassTransit;
using BusService.Messages;
using System.Threading;

namespace BusService.Consumers
{
    class Passenger : Consumes<FareMessage>.Context
    {
        public void Consume(IConsumeContext<FareMessage> context)
        {
            Console.WriteLine(context.Message.FareAmount);
            Thread.Sleep(5000);
        }
    }
}
