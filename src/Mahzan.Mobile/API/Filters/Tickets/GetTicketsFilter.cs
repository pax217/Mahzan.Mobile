using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Filters.Tickets
{
    public class GetTicketsFilter
    {
        public Guid? TicketsId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
