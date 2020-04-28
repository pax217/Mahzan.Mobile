using Mahzan.Mobile.API.Results._Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Results.ProductUnits
{
    public class PostProductUnitsResult:Result
    {
        public Entities.ProductUnits ProductUnit { get; set; }
    }
}
