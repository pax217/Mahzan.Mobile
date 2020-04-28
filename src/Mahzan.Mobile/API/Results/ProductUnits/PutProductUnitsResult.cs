using Mahzan.Mobile.API.Results._Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Results.ProductUnits
{
    public class PutProductUnitsResult:Result
    {
        public Entities.ProductUnits ProductUnits { get; set; }
    }
}
