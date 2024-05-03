using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Common.Events.Payment
{
    public class PaymentComplatedEvent
    {
        public int OrderId { get; set; }
        public int BuyerId { get; set; }
    }
}
