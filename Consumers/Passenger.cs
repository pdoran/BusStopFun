using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassTransit;
using BusService.Messages;
using System.Threading;
using log4net;

namespace BusService.Consumers
{
    public class Passenger : Consumes<FareMessage>.Context
    {
        private ILog _logger;
        public Passenger(ILog _logger) {
            this._logger = _logger;
        }
        public void Consume(IConsumeContext<FareMessage> message) {
            Console.WriteLine(message.Message.FareAmount);
            _logger.DebugFormat("Wow this is slick! {0}", message.Message.FareAmount);
            Thread.Sleep(5000);
            
        }
    }
}
