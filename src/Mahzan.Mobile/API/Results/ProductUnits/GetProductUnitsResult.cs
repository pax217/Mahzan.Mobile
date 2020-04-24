using Mahzan.Mobile.API.Results._Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Results.ProductUnits
{
    public class GetProductUnitsResult: Result
    {
        public List<Entities.ProductUnits> ProductUnits { get; set; }


    }
}
