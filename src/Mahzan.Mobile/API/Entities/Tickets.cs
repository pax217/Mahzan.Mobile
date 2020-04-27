using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Entities
{
    public class Tickets
    {
        #region Properties


        public Guid TicketsId { get; set; }


        public DateTime CreatedAt { get; set; }


        public decimal Total { get; set; }

        public decimal? CashPayment { get; set; }

        public decimal? CashExchange { get; set; }


        public int TotalProducts { get; set; }

        public string BarCode { get; set; }


        public Guid AspNetUsersId { get; set; }


        public bool Active { get; set; }
        #endregion

        #region Relations

        public Guid PointsOfSalesId { get; set; }
        //public PointsOfSales PointsOfSales { get; set; }


        public Guid PaymentTypesId { get; set; }
        //public PaymentTypes PaymentTypes { get; set; }


        public List<TicketDetail> TicketDetails { get; set; }
        #endregion
    }
}
