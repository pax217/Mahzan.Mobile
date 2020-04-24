using Mahzan.Mobile.API.Results._Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Results.Companies
{
    public class GetCompaniesResult:Result
    {
        public List<Entities.Companies> Companies { get; set; }
    }
}
