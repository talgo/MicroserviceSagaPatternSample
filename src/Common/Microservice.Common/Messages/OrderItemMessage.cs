﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Common.Messages
{
    public class OrderItemMessage
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
