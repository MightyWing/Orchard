using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mighty.Shop.Models
{
    public class OrderDetailRecord
    {
        public virtual int Id { get; set; }
        public virtual int OrderRecord_Id { get; set; }
        public virtual int ProductId { get; set; }
        public virtual int Quantity { get; set; }
        public virtual decimal UnitPrice { get; set; }
        public virtual decimal VatRate { get; set; }

        //private decimal unitVat;
        //public virtual decimal UnitVat
        //{
        //    get { return UnitPrice * VatRate; }
        //    set { unitVat = value; }
        //}
        public virtual decimal GetUnitVat()
        {
            return UnitPrice * VatRate;
        }

        //private decimal vat;
        //public virtual decimal Vat
        //{
        //    get { return UnitVat * Quantity; }
        //    set { vat = value; }
        //}
        public virtual decimal GetVat()
        {
            return GetUnitVat() * Quantity;
        }

        //private decimal subTotal;
        //public virtual decimal SubTotal
        //{
        //    get { return UnitPrice * Quantity; }
        //    set { subTotal = value; }
        //}
        public virtual decimal GetSubTotal()
        {
            return UnitPrice * Quantity;
        }

        //private decimal total;
        //public virtual decimal Total
        //{
        //    get { return SubTotal + Vat; }
        //    set { total = value; }
        //}
        public virtual decimal GetTotal()
        {
            return GetSubTotal() + GetVat();
        }
    }

}