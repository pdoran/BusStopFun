using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusService.Messages
{
    public class FareMessageImpl : FareMessage
    {
        public decimal FareAmount { get; set; } 
        public Guid CorrelationId { get; set; }
    }
}
