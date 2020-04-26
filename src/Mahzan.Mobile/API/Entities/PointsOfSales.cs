using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Entities
{
    public class PointsOfSales
    {
        public Guid PointsOfSalesId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        //Stores
        public Guid StoresId { get; set; }

        public Guid MembersId { get; set; }
    }
}
