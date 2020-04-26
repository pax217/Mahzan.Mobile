using Mahzan.Mobile.API.Results._Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Results.Tickets
{
    public class PostTicketCloseSaleResult:Result
    {
        public Entities.Tickets Ticket { get; set; }
    }
}
