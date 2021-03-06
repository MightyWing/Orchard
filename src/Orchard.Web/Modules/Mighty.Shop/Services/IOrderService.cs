﻿using System.Collections.Generic;
using System.Linq;
using Mighty.Shop.Extensions;
using Mighty.Shop.Models;
using Orchard;

namespace Mighty.Shop.Services
{
    public interface IOrderService : IDependency
    {
        /// <summary>
        /// Creates a new order based on the specified ShoppingCartItems
        /// </summary>
        OrderRecord CreateOrder(int customerId, IEnumerable<ShoppingCartItem> items);

        /// <summary>
        /// Gets a list of ProductParts from the specified list of OrderDetails. Useful if you need to use the product as a ProductPart (instead of just having access to the ProductRecord instance).
        /// </summary>
        IEnumerable<ProductPart> GetProducts(IEnumerable<OrderDetailRecord> orderDetails);

        OrderRecord GetOrderByNumber(string orderNumber);
        void UpdateOrderStatus(OrderRecord order, PaymentResponse paymentResponse);
        IEnumerable<OrderRecord> GetOrders(int customerId);
        IQueryable<OrderRecord> GetOrders();
        OrderRecord GetOrder(int id);
    }
}