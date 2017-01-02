using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mighty.Shop.Models
{
    public class AddressPartRecord : ContentPartRecord
    {
        public virtual int CustomerId { get; set; }
        public virtual string Type { get; set; }
    }
}