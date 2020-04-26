using Mahzan.Mobile.API.Filters.EmployeesStores;
using Mahzan.Mobile.API.Results.EmployeesStores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Mobile.API.Interfaces.EmployeesStores
{
    public interface IEmployeesStoresService
    {
        Task<GetEmployeesStoresResult> Get(GetEmployeesStoresFilter getEmployeesStoresFilter);
    }
}
