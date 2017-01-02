using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mighty.Shop.Models;
using System.Web.Mvc;

namespace Mighty.Shop.Extensions
{
    public class PaymentRequest
    {
        public OrderRecord Order { get; private set; }
        public bool WillHandlePayment { get; set; }
        public ActionResult ActionResult { get; set; }

        public PaymentRequest(OrderRecord order)
        {
            Order = order;
        }
    }
}