﻿using Microservice.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Common.Events.Payment
{
    public class PaymentFailedEvent
    {
        public int OrderId { get; set; }
        public int BuyerId { get; set; }
        public string Message { get; set; }

        public List<OrderItemMessage> OrderItemMessages { get; set; }
    }
}
