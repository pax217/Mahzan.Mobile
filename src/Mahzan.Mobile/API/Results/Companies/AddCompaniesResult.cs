using Mahzan.Mobile.API.Results._Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Results.Companies
{
    public class AddCompaniesResult:Result
    {
        public Entities.Companies Company { get; set; }
    }
}
