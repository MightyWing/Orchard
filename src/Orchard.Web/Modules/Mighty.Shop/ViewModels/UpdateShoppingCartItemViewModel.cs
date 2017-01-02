using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mighty.Shop.ViewModels
{
    public class UpdateShoppingCartItemViewModel
    {
        public decimal ProductId { get; set; }
        public bool IsRemoved { get; set; }
        public int Quantity { get; set; }
    }
}