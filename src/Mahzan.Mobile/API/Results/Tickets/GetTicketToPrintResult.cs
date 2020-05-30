using Mahzan.Mobile.API.Results._Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Mobile.API.Results.Tickets
{
    public class GetTicketToPrintResult:Result
    {
        public string Ticket { get; set; }
    }
}
