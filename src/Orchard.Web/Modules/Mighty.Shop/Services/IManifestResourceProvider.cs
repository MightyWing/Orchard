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
        }
    }

}