using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mighty.Shop.Models
{
    public class UpdateShoppingCartItemViewModel
    {
        public int ProductId { get; set; }
        public bool IsRemoved { get; set; }
        public int Quantity { get; set; }
    }
}