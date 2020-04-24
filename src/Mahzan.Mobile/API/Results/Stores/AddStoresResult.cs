using Mahzan.Mobile.API.Results._Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Results.Stores
{
    public class AddStoresResult:Result
    {
        public Entities.Stores Store { get; set; }
    }
}
