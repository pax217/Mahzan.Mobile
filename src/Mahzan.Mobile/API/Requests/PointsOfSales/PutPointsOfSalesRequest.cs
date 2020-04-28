using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Requests.PointsOfSales
{
    public class PutPointsOfSalesRequest
    {
        public Guid PointOfSalesId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
}
