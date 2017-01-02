using Mighty.Shop.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Mighty.Shop.Handlers
{
    public class CustomerHandler : ContentHandler
    {
        public CustomerHandler(IRepository<CustomerRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
            Filters.Add(new ActivatingFilter<UserPart>("Customer"));

            OnGetContentItemMetadata<CustomerPart>((context, part) => {

                if (context.Metadata.EditorRouteValues == null)
                {
                    context.Metadata.EditorRouteValues = new RouteValueDictionary {
                        {"Area", "Orchard.Webshop"},
                        {"Controller", "CustomerAdmin"},
                        {"Action", "Edit"},
                        {"Id", context.ContentItem.Id}
                    };
                }

                if (context.Metadata.RemoveRouteValues == null)
                {
                    context.Metadata.RemoveRouteValues = new RouteValueDictionary {
                        {"Area", "Orchard.Webshop"},
                        {"Controller", "CustomerAdmin"},
                        {"Action", "Edit"},
                        {"Id", context.ContentItem.Id}
                    };
                }

            });
        }
    }
}