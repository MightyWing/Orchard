using Mighty.Shop.Drivers;
using Mighty.Shop.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mighty.Shop.Drivers
{
    public class CustomerDriver : ContentPartDriver<CustomerPart>
    {
        protected override DriverResult Editor(CustomerPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);

            var user = part.User;
            updater.TryUpdateModel(user, Prefix, new[] { "Email" }, null);

            return Editor(part, shapeHelper);

        }
    }
}