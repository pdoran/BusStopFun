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
        public void Consume(IConsumeContext<FareMessage> message) {
            Console.WriteLine(message.Message.FareAmount);
            Thread.Sleep(5000);
        }
    }
}
