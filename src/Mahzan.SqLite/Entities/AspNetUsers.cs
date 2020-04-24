using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.SqLite.Entities
{
    public class AspNetUsers
    {
        public Guid AspNetUsersId { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public Guid EmployeesId { get; set; }

        //Store
        public Guid StoresId { get; set; }
        public string StoreName { get; set; }

        //TPV
        public Guid PointsOfSalesId { get; set; }
        public string PointOfSaleName { get; set; }

    }
}
