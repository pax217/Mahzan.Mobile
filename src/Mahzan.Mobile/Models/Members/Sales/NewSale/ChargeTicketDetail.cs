using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.Models.Members.Sales.NewSale
{
    public class ChargeTicketDetail
    {
        public Guid TicketsClosedId { get; set; }

        public decimal Total { get; set; }

        public decimal? CashPayment { get; set; }

        public decimal? CashExchange { get; set; }

        public int TotalProducts { get; set; }
    }
}
