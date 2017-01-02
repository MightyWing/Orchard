using Orchard.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mighty.Shop.Extensions
{
    public interface IPaymentServiceProvider : IEventHandler
    {
        void RequestPayment(PaymentRequest e);
        void ProcessResponse(PaymentResponse e);
    }
}