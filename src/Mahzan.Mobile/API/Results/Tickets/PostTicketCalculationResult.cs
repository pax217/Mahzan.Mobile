using Mahzan.Mobile.API.DTO.Tickets;
using Mahzan.Mobile.API.Results._Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Results.Tickets
{
    public class PostTicketCalculationResult:Result
    {
        public decimal Total { get; set; }

        public int TotalProducts { get; set; }

        public List<PostTicketCalculationDetailDto> PostTicketDetailDto { get; set; }

        public List<TicketDetailCalculationTaxesDto> TicketDetailTaxesDto { get; set; }
    }
}
