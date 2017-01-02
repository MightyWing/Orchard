using Mighty.Shop.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mighty.Shop.Drivers
{
    public class CustomerPartDriver : ContentPartDriver<CustomerPart>
    {

        protected override string Prefix
        {
            get { return "Customer"; }
        }

        protected override DriverResult Editor(CustomerPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Customer_Edit", () => shapeHelper.EditorTemplate(TemplateName: "Parts/Customer", Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(CustomerPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}