﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassTransit;
namespace BusService.Messages
{
    public interface FareMessage : CorrelatedBy<Guid>
    {
        decimal FareAmount { get; set; }
    }
}
