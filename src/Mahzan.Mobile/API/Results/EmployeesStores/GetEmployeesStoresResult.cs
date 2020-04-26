using Mahzan.Mobile.API.Results._Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Results.EmployeesStores
{
    public class GetEmployeesStoresResult:Result
    {
        public List<Entities.Stores> Stores { get; set; }
    }
}
