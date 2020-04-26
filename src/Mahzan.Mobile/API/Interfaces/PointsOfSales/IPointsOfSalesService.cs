using Mahzan.Mobile.API.Filters.PointsOfSales;
using Mahzan.Mobile.API.Results.PointsOfSales;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Mobile.API.Interfaces.PointsOfSales
{
    public interface IPointsOfSalesService
    {
        Task<GetPointsOfSalesResult> Get(GetPointsOfSalesFilter getPointsOfSalesFilter);
    }
}
