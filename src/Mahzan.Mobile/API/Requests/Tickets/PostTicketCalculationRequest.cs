using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Requests.Tickets
{
    public class PostTicketCalculationRequest
    {

        public Guid StoresId { get; set; }

        public Guid PointsOfSalesId { get; set; }

        public Guid PaymentTypesId { get; set; }

        public decimal? CashPayment { get; set; }

        public List<PostTicketCalculationDetailRequest> PostTicketCalculationDetailRequest { get; set; }
    }

    public class PostTicketCalculationDetailRequest
    {
        public Guid ProductsId { get; set; }

        public int Quantity { get; set; }

    }
}
