using Mahzan.Mobile.API.Results._Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Results.Stores
{
    public class GetStoresResult:Result
    {
        public List<Entities.Stores> Stores { get; set; }
        public Paging.Paging Paging { get; set; }
    }
}
