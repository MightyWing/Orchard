using Orchard.UI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mighty.Shop
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            // Create and add a new manifest
            var manifest = builder.Add();

            // Define a "common" style sheet
            manifest.DefineStyle("Mighty.Shop.Common").SetUrl("common.css");

            // Define the "shoppingcart" style sheet
            manifest.DefineStyle("Mighty.Shop.ShoppingCart").SetUrl("shoppingcart.css").SetDependencies("Mighty.Shop.Common");
            manifest.DefineStyle("Mighty.Shop.ShoppingCartWidget").SetUrl("shoppingcartwidget.css").SetDependencies("Webshop.Common");
            manifest.DefineScript("jQuery").SetUrl("jquery-1.9.1.min.js", "jquery-1.9.1.js").SetVersion("1.9.1");
 
            manifest.DefineStyle("Mighty.Shop.Checkout.Summary").SetUrl("checkout-summary.css").SetDependencies("Mighty.Shop.Common");
            manifest.DefineScript("Mighty.Shop.ShoppingCart").SetUrl("shoppingcart.js").SetDependencies("jQuery", "jQuery_LinqJs", "ko");
            manifest.DefineStyle("Mighty.Shop.Checkout.Summary").SetUrl("checkout-summary.css").SetDependencies("Mighty.Shop.Common");

            manifest.DefineStyle("Mighty.Shop.Order").SetUrl("order.css").SetDependencies("Mighty.Shop.Common");
            manifest.DefineStyle("Mighty.Shop.SimulatedPSP").SetUrl("simulated-psp.css").SetDependencies("Mighty.Shop.Common");
        }
    }

}