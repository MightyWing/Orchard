using Orchard;
using Orchard.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Mighty.Shop.Models;

namespace Mighty.Shop.Services
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly IWorkContextAccessor _workContextAccessor;
        private readonly IContentManager _contentManager;
        public IEnumerable<ShoppingCartItem> Items { get { return ItemsInternal.AsReadOnly(); } }

        private HttpContextBase HttpContext
        {
            get { return _workContextAccessor.GetContext().HttpContext; }
        }

        private List<ShoppingCartItem> ItemsInternal
        {
            get
            {
                var items = (List<ShoppingCartItem>)HttpContext.Session["ShoppingCart"];

                if (items == null)
                {
                    items = new List<ShoppingCartItem>();
                    HttpContext.Session["ShoppingCart"] = items;
                }

                return items;
            }
        }

        public ShoppingCart(IWorkContextAccessor workContextAccessor, IContentManager contentManager)
        {
            _workContextAccessor = workContextAccessor;
            _contentManager = contentManager;
        }
        public void AddRange(IEnumerable<ShoppingCartItem> items) {

        }
        public void Add(int productId, int quantity = 1)
        {
            var item = Items.SingleOrDefault(x => x.ProductId == productId);

            if (item == null)
            {
                item = new ShoppingCartItem(productId, quantity);
                ItemsInternal.Add(item);
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public void Remove(int productId)
        {
            var item = Items.SingleOrDefault(x => x.ProductId == productId);

            if (item == null)
                return;

            ItemsInternal.Remove(item);
        }

        public ProductPart GetProduct(int productId)
        {
            return _contentManager.Get<ProductPart>(productId);
        }
        public IEnumerable<ProductQuantity> GetProducts()
        {
            // Get a list of all product IDs from the shopping cart
            var ids = Items.Select(x => x.ProductId).ToList();

            // Load all product parts by the list of IDs
            var productParts = _contentManager.GetMany<ProductPart>(ids, VersionOptions.Latest, QueryHints.Empty).ToArray();
            var dd = productParts.ToList();
            // Create a LINQ query that projects all items in the shoppingcart into shapes
            var query = from item in Items
                        from productPart in productParts
                        where productPart.Id == item.ProductId
                        select new ProductQuantity
                        {
                            ProductPart = productPart,
                            Quantity = item.Quantity
                        };
            var aa = query.ToList();
            return query;
        }
        public void UpdateItems()
        {
            ItemsInternal.RemoveAll(x => x.Quantity == 0);
        }

        public decimal Subtotal()
        {
            return Items.Select(x => GetProduct(x.ProductId).UnitPrice * x.Quantity).Sum();
        }

        public decimal Vat()
        {
            return Subtotal() * .19m;
        }

        public decimal Total()
        {
            return Subtotal() + Vat();
        }

        public int ItemCount()
        {
            return Items.Sum(x => x.Quantity);
        }

        public void Clear()
        {
            ItemsInternal.Clear();
            UpdateItems();
        }
 
    }
}