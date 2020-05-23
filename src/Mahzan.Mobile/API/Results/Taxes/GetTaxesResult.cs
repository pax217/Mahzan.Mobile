using Mahzan.Mobile.API.Results._Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Results.Taxes
{
    public class GetTaxesResult:Result
    {
        public List<API.Entities.Taxes> Taxes { get; set; }
    }
}
