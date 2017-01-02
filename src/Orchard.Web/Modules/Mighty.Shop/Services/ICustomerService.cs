using Mighty.Shop.Models;
using Orchard;
using Orchard.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mighty.Shop.Services
{
    public interface ICustomerService : IDependency
    {
        CustomerPart CreateCustomer(string email, string password);
        AddressPart GetAddress(int customerId, string addressType);
        AddressPart CreateAddress(int customerId, string addressType);
        IContentQuery<CustomerPart> GetCustomers();
        CustomerPart GetCustomer(int id);
        IEnumerable<AddressPart> GetAddresses(int customerId);
        AddressPart GetAddress(int id);
    }
}