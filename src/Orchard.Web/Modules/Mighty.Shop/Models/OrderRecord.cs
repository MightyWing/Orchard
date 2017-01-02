using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mighty.Shop.Models
{
    public class OrderRecord
    {
        public virtual int Id { get; set; }
        public virtual int CustomerId { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual decimal SubTotal { get; set; }
        public virtual decimal Vat { get; set; }
        public virtual OrderStatus Status { get; set; }
        public virtual IList<OrderDetailRecord> Details { get; private set; }
        public virtual string PaymentServiceProviderResponse { get; set; }
        public virtual string PaymentReference { get; set; }
        public virtual DateTime? PaidAt { get; set; }
        public virtual DateTime? CompletedAt { get; set; }
        public virtual DateTime? CancelledAt { get; set; }

        ////private decimal total;
        //public virtual decimal Total
        //{
        //    get { return SubTotal + Vat; }
        //    //private set { total = value; }
        //}

        public virtual decimal GetTotal()
        {
            return SubTotal + Vat;
        }

        ////private string number;
        //public virtual string Number
        //{
        //    get { return (Id + 1000).ToString(CultureInfo.InvariantCulture); }
        //    //private set { number = value; }
        //}
        public virtual string GetNumber()
        {
            return (Id + 1000).ToString(CultureInfo.InvariantCulture);
        }

        public OrderRecord()
        {
            Details = new List<OrderDetailRecord>();
        }

        public virtual void UpdateTotals()
        {
            var subTotal = 0m;
            var vat = 0m;

            foreach (var detail in Details)
            {
                subTotal += detail.GetSubTotal();
                vat += detail.GetVat();
            }

            SubTotal = subTotal;
            Vat = vat;
        }
    }

}