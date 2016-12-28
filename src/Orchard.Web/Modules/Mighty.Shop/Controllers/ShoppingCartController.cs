using Mighty.Shop.Services;
using Orchard;
using Orchard.Mvc;
using Orchard.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mighty.Shop.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        private readonly IShoppingCart _shoppingCart;
        private readonly IOrchardServices _services;

        public ShoppingCartController(IShoppingCart shoppingCart, IOrchardServices services)
        {
            _shoppingCart = shoppingCart;
            _services = services;
        }

        [HttpPost]
        public ActionResult Add(int id)
        {

            // Add the specified content id to the shopping cart with a quantity of 1.
            _shoppingCart.Add(id, 1);

            // Redirect the user to the Index action (yet to be created)
            return RedirectToAction("Index");
        }
        [Themed]
        public ActionResult Index()
        {

            // Create a new shape using the "New" property of IOrchardServices.
            var shape = _services.New.ShoppingCart();

            // Return a ShapeResult
            return new ShapeResult(this, shape);
        }
    }
}