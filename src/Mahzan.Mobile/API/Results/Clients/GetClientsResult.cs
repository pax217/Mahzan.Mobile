using Mahzan.Mobile.API.Results._Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Results.Clients
{
    public class GetClientsResult:Result
    {
        public List<API.Entities.Clients> Clients { get; set; }
    }
}
