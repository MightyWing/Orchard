using Mighty.Shop.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mighty.Shop.Handlers
{
    public class ProductPartHandler : ContentHandler
    {
        public ProductPartHandler(IRepository<ProductPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}