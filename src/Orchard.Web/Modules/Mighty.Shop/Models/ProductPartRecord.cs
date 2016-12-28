using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mighty.Shop.Models
{
    public class ProductPartRecord : ContentPartRecord
    {
        public virtual decimal UnitPrice { get; set; }
        public virtual string Sku { get; set; }
    }
}