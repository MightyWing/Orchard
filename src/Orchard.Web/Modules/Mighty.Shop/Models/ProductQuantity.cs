using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mighty.Shop.Models
{
    public sealed class ProductQuantity
    {
        public ProductPart ProductPart { get; set; }
        public int Quantity { get; set; }
    }
}