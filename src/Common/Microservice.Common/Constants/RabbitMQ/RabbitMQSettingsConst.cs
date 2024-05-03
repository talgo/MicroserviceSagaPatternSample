﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Common.Constants.RabbitMQ
{
    public class RabbitMQSettingsConst
    {
        public const string StockOrderCreatedEventQueueName = "stock-order-created-queue";

        public const string StockReservedEventQueueName = "stock-reserved-queue";

        public const string OrderPaymentCompletedEventQueueName = "order-payment-completed-queue";

        public const string OrderPaymentFailedEventQueueName = "order-payment-failed-queue";

        public const string OrderStockNotReservedEventQueueName = "order-stock-not-reserved-queue";

        public const string StockPaymentFailedEventQueueName = "stock-payment-failed-queue";
    }
}