using Mahzan.Mobile.API.Results._Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Results.Tickets
{
    public class GetTicketsResult:Result
    {
        public List<Entities.Tickets> Tickets { get; set; }
    }
}
