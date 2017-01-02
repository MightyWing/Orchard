﻿using Orchard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Orchard.Mvc;
using Orchard.Themes;
using Orchard.Localization;
using Orchard.Security;
using Mighty.Shop.ViewModels;
using Mighty.Shop.Services;
using Mighty.Shop.Models;
using Mighty.Shop.Helpers;
using Orchard.ContentManagement;
using Orchard.DisplayManagement;
using Mighty.Shop.Extensions;

namespace Mighty.Shop.Controllers
{
    public class OrderController : Controller
    {
        private readonly dynamic _shapeFactory;
        private readonly IOrderService _orderService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IShoppingCart _shoppingCart;
        private readonly ICustomerService _customerService;
        private readonly IEnumerable<IPaymentServiceProvider> _paymentServiceProviders;
        private readonly Localizer _t;

        public OrderController(
            IShapeFactory shapeFactory,
            IOrderService orderService,
            IAuthenticationService authenticationService,
            IShoppingCart shoppingCart,
            ICustomerService customerService,
            IEnumerable<IPaymentServiceProvider> paymentServiceProviders)
        {
            _shapeFactory = shapeFactory;
            _orderService = orderService;
            _authenticationService = authenticationService;
            _shoppingCart = shoppingCart;
            _customerService = customerService;
            _paymentServiceProviders = paymentServiceProviders;
            //_paymentServiceProvider = new SimulatedPaymentServiceProvider();
            _t = NullLocalizer.Instance;
        }

        [Themed, HttpPost]
        public ActionResult Create()
        {

            var user = _authenticationService.GetAuthenticatedUser();

            if (user == null)
                throw new OrchardSecurityException(_t("Login required"));

            var customer = user.ContentItem.As<CustomerPart>();

            if (customer == null)
                throw new InvalidOperationException("The current user is not a customer");

            var order = _orderService.CreateOrder(customer.Id, _shoppingCart.Items);

            // Fire the PaymentRequest event
            var paymentRequest = new PaymentRequest(order);

            foreach (var handler in _paymentServiceProviders)
            {
                handler.RequestPayment(paymentRequest);

                // If the handler responded, it will set the action result
                if (paymentRequest.WillHandlePayment)
                {
                    return paymentRequest.ActionResult;
                }
            }

            // If we got here, no PSP handled the OrderCreated event, so we'll just display the order.
            var shape = _shapeFactory.Order_Created(
                Order: order,
                Products: _orderService.GetProducts(order.Details).ToArray(),
                Customer: customer,
                InvoiceAddress: (dynamic)_customerService.GetAddress(user.Id, "InvoiceAddress"),
                ShippingAddress: (dynamic)_customerService.GetAddress(user.Id, "ShippingAddress")
            );
            return new ShapeResult(this, shape);
        }

        [Themed]
        public ActionResult PaymentResponse()
        {

            var args = new PaymentResponse(HttpContext);

            foreach (var handler in _paymentServiceProviders)
            {
                handler.ProcessResponse(args);

                if (args.WillHandleResponse)
                    break;
            }

            if (!args.WillHandleResponse)
                throw new OrchardException(_t("Such things mean trouble"));

            var order = _orderService.GetOrderByNumber(args.OrderReference);
            _orderService.UpdateOrderStatus(order, args);

            if (order.Status == OrderStatus.Paid)
            {
                // Send some notification mail message to the customer that the order was paid.
                // We may also initiate the shipping process from here
            }

            return new ShapeResult(this, _shapeFactory.Order_PaymentResponse(Order: order, PaymentResponse: args));
        }
    }

}