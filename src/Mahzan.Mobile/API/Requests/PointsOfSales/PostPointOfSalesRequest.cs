using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Requests.PointsOfSales
{
    public class PostPointOfSalesRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid StoresId { get; set; }
    }
}
