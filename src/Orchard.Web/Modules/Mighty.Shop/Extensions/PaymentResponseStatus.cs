using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mighty.Shop.Extensions
{
    public enum PaymentResponseStatus
    {
        Success,
        Failed,
        Cancelled,
        Exception
    }
}