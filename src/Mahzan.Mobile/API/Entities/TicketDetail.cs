using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Entities
{
    public class TicketDetail
    {
        public Guid TicketDetailId { get; set; }

        public Guid ProductsId { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal Amount { get; set; }
    }
}
